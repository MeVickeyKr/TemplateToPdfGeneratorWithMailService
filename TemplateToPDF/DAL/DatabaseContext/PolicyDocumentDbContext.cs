using System;
using System.Collections.Generic;
using TemplateToPDF.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace TemplateToPDF.DAL.DatabaseContext
{
    public class PolicyDocumentDbContext : DbContext
    {
        public PolicyDocumentDbContext()
        {
        }

        public PolicyDocumentDbContext(DbContextOptions<PolicyDocumentDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HtmlTemplateEntity> HtmlTemplates { get; set; }

        public virtual DbSet<PolicyPdfRecord> PolicyPdfRecord { get; set; }

        public virtual DbSet<UserPolicyDetailEntity> UserPolicyDetails { get; set; }


    }

}
