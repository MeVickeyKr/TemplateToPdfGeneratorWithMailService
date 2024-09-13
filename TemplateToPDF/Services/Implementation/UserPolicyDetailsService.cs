using TemplateToPDF.DAL.Entities;
using TemplateToPDF.DAL.Repository.Implementations;
using TemplateToPDF.DAL.Repository.Interface;
using TemplateToPDF.Services.ApiResquestModel;
using TemplateToPDF.Services.Interface;

namespace TemplateToPDF.Services.Implementation
{
    public class UserPolicyDetailsService : IUserPolicyDetailsService
    {
        private readonly IUserPolicyDetailsRepository _userPolicyDetailsRepository;
        private readonly IEPolicyKitDocumentGenerationService _ePolicyKitDocumentGenerationService;
        public UserPolicyDetailsService(IUserPolicyDetailsRepository userPolicyDetailsRepository , IEPolicyKitDocumentGenerationService ePolicyKitDocumentGenerationService )
        {
            _userPolicyDetailsRepository = userPolicyDetailsRepository;
            _ePolicyKitDocumentGenerationService = ePolicyKitDocumentGenerationService;
        }

        public async Task<UserPolicyDetailEntity> CreateItemAsync(UserPolicyDetailRequestModel userPolicyDetailRequestModel)
        {
            UserPolicyDetailEntity userPolicyDetailEntity = new UserPolicyDetailEntity()
            {
                Name = userPolicyDetailRequestModel.Name,

                PolicyNumber = userPolicyDetailRequestModel.PolicyNumber,

                Age = userPolicyDetailRequestModel.Age,

                Salary = userPolicyDetailRequestModel.Salary,

                Occupation = userPolicyDetailRequestModel.Occupation,

                PolicyExpiryDate = userPolicyDetailRequestModel.PolicyExpiryDate,


                ProductCode = userPolicyDetailRequestModel.ProductCode,

                EmailAddress = userPolicyDetailRequestModel.EmailAddress,

            };
            await _userPolicyDetailsRepository.PostPolicyDetailsToDb(userPolicyDetailEntity);
            await  _ePolicyKitDocumentGenerationService.GenerateAndSavePdfAsync(userPolicyDetailEntity);
            return userPolicyDetailEntity;
        }
    }
}
