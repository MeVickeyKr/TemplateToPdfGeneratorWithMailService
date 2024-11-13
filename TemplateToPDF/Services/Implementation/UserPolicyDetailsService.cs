using TemplateToPDF.DAL.Entities;
using TemplateToPDF.DAL.Repository.Interface;
using TemplateToPDF.Services.ApiResquestModel;
using TemplateToPDF.Services.Interface;

namespace TemplateToPDF.Services.Implementation
{
    public class UserPolicyDetailsService : IUserPolicyDetailsService
    {
        private readonly IUserPolicyDetailsRepository _userPolicyDetailsRepository;
        private readonly IEPolicyKitDocumentGenerationService _ePolicyKitDocumentGenerationService;
        private readonly IMessagingRepository _messagingRepository;
        public UserPolicyDetailsService(IUserPolicyDetailsRepository userPolicyDetailsRepository, IEPolicyKitDocumentGenerationService ePolicyKitDocumentGenerationService , IMessagingRepository messagingRepository)
        {
            _userPolicyDetailsRepository = userPolicyDetailsRepository;
            _ePolicyKitDocumentGenerationService = ePolicyKitDocumentGenerationService;
            _messagingRepository = messagingRepository;
        }

        public async Task<UserPolicyDetailEntity> CreateItemAsync(UserPolicyDetailRequestModel model)
        {
            UserPolicyDetailEntity userPolicyDetailEntity = new UserPolicyDetailEntity()
            {
                Name = model.Name,

                PolicyNumber = model.PolicyNumber,

                Age = model.Age,

                Salary = model.Salary,

                Occupation = model.Occupation,

                PolicyExpiryDate = model.PolicyExpiryDate,

                ProductCode = model.ProductCode,

                EmailAddress = model.EmailAddress,

            };
            var user = await _userPolicyDetailsRepository.PostPolicyDetailsToDb(userPolicyDetailEntity);

            // mapping messaging Table
            MessagingEntity messagingEntity = new MessagingEntity()
            {
                PolicyNumber = $"{user.PolicyNumber}-{user.ProductCode}",
                Destination = user.EmailAddress, 
                DestinationCC = null,
                DestinationBCC = null,
                Body = Extensions.Body.Replace("User" , user.Name),
                Attempt=0,
                MaxAttempt=Extensions.MaxAttempt,
                isDeleted=false,
                isSent=false,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedDateTime = DateTime.UtcNow,
                LastAttempt = DateTime.UtcNow,
            };
            await _messagingRepository.AddAsync(messagingEntity);
            await _ePolicyKitDocumentGenerationService.GenerateAndSavePdfAsync(userPolicyDetailEntity);
            return userPolicyDetailEntity;
        }
    }
}
