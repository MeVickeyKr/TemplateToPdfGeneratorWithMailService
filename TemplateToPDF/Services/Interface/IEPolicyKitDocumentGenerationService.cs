using TemplateToPDF.DAL.Entities;

namespace TemplateToPDF.Services.Interface
{
    public interface IEPolicyKitDocumentGenerationService
    {
        public  Task GenerateAndSavePdfAsync(UserPolicyDetailEntity userpolicyDetails);
    }
}
