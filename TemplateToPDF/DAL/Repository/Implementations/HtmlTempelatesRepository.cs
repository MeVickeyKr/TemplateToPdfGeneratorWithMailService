

using Microsoft.EntityFrameworkCore;
using TemplateToPDF.DAL.DatabaseContext;
using TemplateToPDF.DAL.Repository.Interface;
namespace TemplateToPDF.DAL.Repository.Implementations
{
    public class HtmlTempelatesRepository : IHtmlTempelatesRepository
    {
        private readonly PolicyDocumentDbContext _policyDocumentDbContext;
        public HtmlTempelatesRepository(PolicyDocumentDbContext policyDocumentDbContext)
        {
            _policyDocumentDbContext = policyDocumentDbContext;  
        }

        public async Task<string> GetTemplateAsync(string documentId)
        {
            // Assuming "HtmlTemplate" is the entity class and "Content" is the string field storing the HTML content
            var template = await _policyDocumentDbContext.HtmlTemplates.FirstOrDefaultAsync( p =>
            p.DocumnetCode == documentId
            );

            // Check if the template exists
            if (template == null)
            {
                return null; // Or return a default value or throw an exception, depending on your use case
            }

            // Return the "Content" field (assuming that’s where the HTML is stored)
            return template.Content;
        }
    }
}
