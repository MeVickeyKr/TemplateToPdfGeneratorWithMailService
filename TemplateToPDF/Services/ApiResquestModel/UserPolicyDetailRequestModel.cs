namespace TemplateToPDF.Services.ApiResquestModel
{
    public class UserPolicyDetailRequestModel
    {
        public string Name { get; set; } = null!;

        public string PolicyNumber { get; set; } = null!;

        public int Age { get; set; }

        public decimal? Salary { get; set; }

        public string? Occupation { get; set; }

        public DateTime PolicyExpiryDate { get; set; }

        public string ProductCode { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;


    }
}
