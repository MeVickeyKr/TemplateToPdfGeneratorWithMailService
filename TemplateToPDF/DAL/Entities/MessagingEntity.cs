using System.ComponentModel.DataAnnotations;

namespace TemplateToPDF.DAL.Entities
{
 
    public class MessagingEntity
    {
        [Key]
        public string PolicyNumber { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string? DestinationCC {  get; set; } 
        public string? DestinationBCC { get; set; }
        public string Body { get; set; } = null!;
        public int Attempt { get; set; }
        public int MaxAttempt { get; set; }
        public DateTime LastAttempt { get; set; }
        public bool isSent { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDateTime {  get; set; }
        public DateTime UpdatedDateTime { get; set; }



    }
}
