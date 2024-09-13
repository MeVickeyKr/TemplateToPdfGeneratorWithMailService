using TemplateToPDF.DAL.Entities;

namespace TemplateToPDF.DAL.Repository.Interface
{
    public interface IPolicyPdfRecordsRepository
    {


        Task<PolicyPdfRecord> GetDocumentByPolicyNumberAndProductCodeAsync(string policyNumber, string productCode);
        Task SoftDeleteExistingDocumentAsync(string policyNumber, string productCode);
            Task AddDocumentAsync(PolicyPdfRecord document);
            Task SaveChangesAsync();
        

    }
}
