namespace TemplateToPDF.DAL.Repository.Interface
{
    public interface IHtmlTempelatesRepository
    {

        public Task<string> GetTemplateAsync(string id);
    }
}
