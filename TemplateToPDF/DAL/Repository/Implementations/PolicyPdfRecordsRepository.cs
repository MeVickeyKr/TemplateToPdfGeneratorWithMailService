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

        public async Task<PolicyPdfRecordEntity> GetDocumentByObjectCode(string objectCode)
        {
            return await _dbContext.PolicyPdfRecord
                .FirstOrDefaultAsync(d => d.ObjectCode == objectCode) ?? new();
        }

        public async Task SoftDeleteExistingDocumentAsync(string objectCode)
        {
            var existingDocument = await GetDocumentByObjectCode(objectCode);

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
        public async Task AddDocumentAsync(PolicyPdfRecordEntity document)
        {

            string path = @"C:\Users\RemoteState\Desktop\pdf document";
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
