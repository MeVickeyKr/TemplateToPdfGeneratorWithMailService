using TemplateToPDF.DAL.Entities;
using TemplateToPDF.Services.ApiResquestModel;

namespace TemplateToPDF.Services.Interface
{
    public interface IUserPolicyDetailsService
    {
        public Task<UserPolicyDetailEntity> CreateItemAsync(UserPolicyDetailRequestModel userPolicyDetailRequestModel);
    }
}
