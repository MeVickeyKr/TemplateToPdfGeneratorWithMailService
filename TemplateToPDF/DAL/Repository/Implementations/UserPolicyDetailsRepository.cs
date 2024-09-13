using Microsoft.EntityFrameworkCore;
using TemplateToPDF.DAL.DatabaseContext;
using TemplateToPDF.DAL.Entities;
using TemplateToPDF.DAL.Repository.Interface;

namespace TemplateToPDF.DAL.Repository.Implementations
{
    public class UserPolicyDetailsRepository : IUserPolicyDetailsRepository
    {
   
            private readonly PolicyDocumentDbContext _policyDocumentDbContext;
        public UserPolicyDetailsRepository(PolicyDocumentDbContext policyDocumentDbContext)
        {
            _policyDocumentDbContext = policyDocumentDbContext;  
        }




        public async Task<UserPolicyDetailEntity> PostPolicyDetailsToDb(UserPolicyDetailEntity userPolicyDetailEntity)
            {

                await _policyDocumentDbContext.UserPolicyDetails.AddAsync(userPolicyDetailEntity);
                await _policyDocumentDbContext.SaveChangesAsync();
                return userPolicyDetailEntity;
            }
        }
    }

