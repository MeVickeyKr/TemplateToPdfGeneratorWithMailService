using PuppeteerSharp.Media;
using PuppeteerSharp;
using TemplateToPDF.DAL.Entities;
using TemplateToPDF.DAL.Repository.Implementations;
using TemplateToPDF.DAL.Repository.Interface;
using TemplateToPDF.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace TemplateToPDF.Services.Implementation
{
    public class EPolicyKitDocumentGenerationService : IEPolicyKitDocumentGenerationService
    {
        private readonly IPolicyPdfRecordsRepository _policyPdfRecordsRepository;
        private readonly IHtmlTempelatesRepository _htmlTempelatesRepository;
        private readonly IEmailService _emailService;

        public EPolicyKitDocumentGenerationService(IPolicyPdfRecordsRepository policyPdfRecordsRepository, IHtmlTempelatesRepository htmlTempelatesRepository,   IEmailService emailService  )
        {
            _policyPdfRecordsRepository = policyPdfRecordsRepository;
            _htmlTempelatesRepository = htmlTempelatesRepository;
            _emailService = emailService;
           

        }
        public async Task GenerateAndSavePdfAsync(UserPolicyDetailEntity userpolicyDetailsEntity)
        {
            // 1. Populate the HTML template with the user's data
            string htmlTemplate = await _htmlTempelatesRepository.GetTemplateAsync("newhtmltemplate");

            string populatedHtml = htmlTemplate
            .Replace("{{Name}}", userpolicyDetailsEntity.Name)
            .Replace("{{PolicyNumber}}", userpolicyDetailsEntity.PolicyNumber)
            .Replace("{{Age}}", userpolicyDetailsEntity.Age.ToString())
            .Replace("{{Salary}}", userpolicyDetailsEntity.Salary.ToString())
            .Replace("{{Occupation}}", userpolicyDetailsEntity.Occupation)
            .Replace("{{ProductCode}}", userpolicyDetailsEntity.ProductCode)
            .Replace("{{PolicyExpiryDate}}", userpolicyDetailsEntity.PolicyExpiryDate.ToString("yyyy-MM-dd"));


            byte[] pdfBytes = await GeneratePdfAsync(populatedHtml);
            await SavePdfToDatabase(userpolicyDetailsEntity, pdfBytes);



            string recipientEmail = userpolicyDetailsEntity.EmailAddress; // Replace with actual email
            string subject = "Regarding Your Credit Card";
            string message = $"Dear {userpolicyDetailsEntity.Name}, please find your updated  document attached.";
            string attachmentName = $"{userpolicyDetailsEntity.PolicyNumber}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            await _emailService.SendEmailWithAttachmentAsync(recipientEmail, subject, message, pdfBytes, attachmentName);
        }
        private async Task<byte[]> GeneratePdfAsync(string htmlContent)
        {
            object value = await new BrowserFetcher().DownloadAsync();
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            {
                using (var page = await browser.NewPageAsync())
                {
                    await page.SetContentAsync(htmlContent);
                    return await page.PdfDataAsync(new PdfOptions
                    {
                        Format = PaperFormat.A4,
                        PrintBackground = true,
                        MarginOptions = new MarginOptions { Top= "10px" , Bottom="10px"}
                    });
                }
            }
        }
       
        private async Task SavePdfToDatabase(UserPolicyDetailEntity userPolicyDetailsEntity, byte[] pdfBytes)
        {
            var existingDocument = await _policyPdfRecordsRepository.GetDocumentByPolicyNumberAndProductCodeAsync(
              userPolicyDetailsEntity.PolicyNumber,
              userPolicyDetailsEntity.ProductCode
             );

            if (existingDocument != null)
            {
                existingDocument.IsDeleted = true;
                await _policyPdfRecordsRepository.SaveChangesAsync();
            }

            var newDocument = new PolicyPdfRecord
            {
                ObjectCode = $"{userPolicyDetailsEntity.PolicyNumber} - {userPolicyDetailsEntity.ProductCode}",
                ReferenceType = "Policy",
                ReferenceNumber = userPolicyDetailsEntity.PolicyNumber,
                Content = pdfBytes,
                FileName = $"{userPolicyDetailsEntity.PolicyNumber}_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                FileExtension = ".pdf",
                LanguageCode = "km-KH",
                CreatedUser = "System", // Replace with actual user if necessary
                CreatedDateTime = DateTime.Now,
                IsDeleted = false
            };
            await _policyPdfRecordsRepository.AddDocumentAsync(newDocument);
            await _policyPdfRecordsRepository.SaveChangesAsync();
        }
    }
}