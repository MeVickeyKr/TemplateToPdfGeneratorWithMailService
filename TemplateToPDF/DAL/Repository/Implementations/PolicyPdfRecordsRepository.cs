using Microsoft.EntityFrameworkCore;
using TemplateToPDF.DAL.DatabaseContext;
using TemplateToPDF.DAL.Entities;
using TemplateToPDF.DAL.Repository.Interface;


namespace TemplateToPDF.DAL.Repository.Implementations
{
    public class PolicyPdfRecordsRepository : IPolicyPdfRecordsRepository
    {
        private readonly PolicyDocumentDbContext _dbContext;
        public PolicyPdfRecordsRepository(PolicyDocumentDbContext policyDocumentDbContext)
        {
           _dbContext = policyDocumentDbContext;
        }

        public async Task<PolicyPdfRecord> GetDocumentByPolicyNumberAndProductCodeAsync(string policyNumber, string productCode)
        {
            return await _dbContext.PolicyPdfRecord
                .FirstOrDefaultAsync(d => d.ReferenceNumber == policyNumber && d.ObjectCode == $"{policyNumber} - {productCode}");
        }

        public async Task SoftDeleteExistingDocumentAsync(string policyNumber, string productCode)
        {
            var existingDocument = await GetDocumentByPolicyNumberAndProductCodeAsync(policyNumber, productCode);

            if (existingDocument != null)
            {
                existingDocument.IsDeleted = true;
                _dbContext.PolicyPdfRecord.Update(existingDocument);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddDocumentAsync(PolicyPdfRecord document)
        {

            string path = @"C:\Users\vk774\OneDrive\Desktop\Pdfdocument";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string FilePath = Path.Combine(path, "Document.pdf");
            await System.IO.File.WriteAllBytesAsync(FilePath , document.Content);
            await _dbContext.PolicyPdfRecord.AddAsync(document);
        }
    }
}
