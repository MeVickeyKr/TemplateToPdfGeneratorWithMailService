using System.Net.Mail;
using System.Net;
using TemplateToPDF.Services.Interface;
using TemplateToPDF.DAL.Entities;
using TemplateToPDF.DAL.Repository.Interface;

namespace TemplateToPDF.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly IMessagingRepository _messagingRepository;
        private readonly IPolicyPdfRecordsRepository _policyPdfRecordsRepository;


        public EmailService(IMessagingRepository messagingRepository, IPolicyPdfRecordsRepository policyPdfRecordsRepository)
        {
            _smtpServer = "smtp.gmail.com";
            _smtpPort = 587;
            _smtpUsername = "hello.learnnearn@gmail.com";
            _smtpPassword = "peco udeu xfph hhec";
            _messagingRepository = messagingRepository;
            _policyPdfRecordsRepository = policyPdfRecordsRepository;
        }

        public async Task GenerateEmail()
        {
            List<MessagingEntity> entities = await _messagingRepository.GetAllAsync();
            foreach (MessagingEntity entity in entities)
            {
                try
                {
                    var document = await _policyPdfRecordsRepository.GetDocumentByObjectCode(entity.PolicyNumber);
                    await SendEmailWithAttachmentAsync(entity, document);
                    entity.isSent = true;
                }
                catch (Exception ex)
                { 
                    Console.WriteLine(ex.Message);
                }
                entity.Attempt++;
                entity.LastAttempt = DateTime.Now;
            }
            await _messagingRepository.UpdateAllAsync(entities);
        }
        public async Task SendEmailWithAttachmentAsync(MessagingEntity entity, PolicyPdfRecordEntity policyPdf)
        {
            using (var client = new SmtpClient()
            {
                //Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                //EnableSsl = true,

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)

            })
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUsername),
                    Subject = "Regarding Policy Generation",
                    Body = entity.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(entity.Destination);

                // Create a memory stream for the attachment data
                using (var memoryStream = new MemoryStream(policyPdf.Content ))
                {
                    var attachment = new Attachment(memoryStream, policyPdf.FileName, "application/pdf");
                    mailMessage.Attachments.Add(attachment);

                    // Send the email
                    await client.SendMailAsync(mailMessage);
                }
            }
        }
    }

}

