using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TemplateToPDF.DAL.Entities;

public partial class UserPolicyDetailEntity
{
    public string Name { get; set; } = null!;
    [Key]
    public string PolicyNumber { get; set; } = null!;

    public int Age { get; set; }

    public decimal? Salary { get; set; }

    public string? Occupation { get; set; }

    public DateTime PolicyExpiryDate { get; set; }

    public string ProductCode { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;
}
