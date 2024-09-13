using TemplateToPDF.DAL.Entities;

namespace TemplateToPDF.DAL.Repository.Interface
{
    public interface IUserPolicyDetailsRepository
    {
        public Task<UserPolicyDetailEntity> PostPolicyDetailsToDb(UserPolicyDetailEntity UserPolicyDetails);
    }
}
