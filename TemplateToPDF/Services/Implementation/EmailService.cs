using System.Net.Mail;
using System.Net;
using TemplateToPDF.Services.Interface;

namespace TemplateToPDF.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailService()
        {
            _smtpServer = "smtp.gmail.com";
            _smtpPort = 587;
            _smtpUsername = "hello.learnnearn@gmail.com";
            _smtpPassword = "peco udeu xfph hhec";
        }

        public async Task SendEmailWithAttachmentAsync(string recipientEmail, string subject, string message, byte[] attachmentData, string attachmentName)
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
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(recipientEmail);

                // Create a memory stream for the attachment data
                using (var memoryStream = new MemoryStream(attachmentData))
                {
                    var attachment = new Attachment(memoryStream, attachmentName, "application/pdf");
                    mailMessage.Attachments.Add(attachment);

                    // Send the email
                    await client.SendMailAsync(mailMessage);
                }
            }
        }
    }

}

