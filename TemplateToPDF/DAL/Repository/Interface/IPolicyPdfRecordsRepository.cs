using TemplateToPDF.DAL.Entities;

namespace TemplateToPDF.DAL.Repository.Interface
{
    public interface IPolicyPdfRecordsRepository
    {


        Task<PolicyPdfRecordEntity> GetDocumentByObjectCode(string objectCode);
        Task SoftDeleteExistingDocumentAsync(string objectCode);
            Task AddDocumentAsync(PolicyPdfRecordEntity document);
            Task SaveChangesAsync();
        

    }
}
