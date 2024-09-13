using Microsoft.EntityFrameworkCore;
using TemplateToPDF.DAL.DatabaseContext;
using TemplateToPDF.DAL.Repository.Implementations;
using TemplateToPDF.DAL.Repository.Interface;
using TemplateToPDF.Services.Implementation;
using TemplateToPDF.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddTransient<IUserPolicyDetailsRepository , UserPolicyDetailsRepository>();
builder.Services.AddTransient<IUserPolicyDetailsService, UserPolicyDetailsService>();
builder.Services.AddTransient<IHtmlTempelatesRepository, HtmlTempelatesRepository>();
builder.Services.AddTransient<IPolicyPdfRecordsRepository, PolicyPdfRecordsRepository>();
builder.Services.AddTransient<IEPolicyKitDocumentGenerationService , EPolicyKitDocumentGenerationService>();
builder.Services.AddTransient<IEmailService , EmailService>();

builder.Services.AddDbContext<PolicyDocumentDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
