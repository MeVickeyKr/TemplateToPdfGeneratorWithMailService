namespace TemplateToPDF.DAL.Entities;

public partial class HtmlTemplateEntity
{
    public int Id { get; set; }

    public string DocumnetCode { get; set; } = null!;

    public string? FileName { get; set; }

    public string? ContentType { get; set; }

    public string Content { get; set; } = null!;

    public byte[]? ContentBinary { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedUser { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public string? ModifiedUser { get; set; }

    public DateTime? ModifiedDateTime { get; set; }
}
