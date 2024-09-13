namespace TemplateToPDF.Services.Interface
{
    public interface IEmailService
    {
        Task SendEmailWithAttachmentAsync(string recipientEmail, string subject, string message, byte[] attachmentData, string attachmentName);
    }
}
