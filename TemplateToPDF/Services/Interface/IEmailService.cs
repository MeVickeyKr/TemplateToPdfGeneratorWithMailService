using TemplateToPDF.DAL.Entities;

namespace TemplateToPDF.Services.Interface
{
    public interface IEmailService
    {
        Task SendEmailWithAttachmentAsync(MessagingEntity entity, PolicyPdfRecordEntity policyPdf);
    }
}
