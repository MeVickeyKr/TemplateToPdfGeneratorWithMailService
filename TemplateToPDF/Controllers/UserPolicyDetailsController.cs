using Hangfire;
using Microsoft.AspNetCore.Mvc;
using TemplateToPDF.Services.ApiResquestModel;
using TemplateToPDF.Services.Implementation;
using TemplateToPDF.Services.Interface;

namespace TemplateToPDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPolicyDetailsController : ControllerBase
    {
        private readonly IUserPolicyDetailsService _userPolicyDetailsService;
        private readonly ILogger<UserPolicyDetailsController> _logger;
        public UserPolicyDetailsController(IUserPolicyDetailsService userPolicyDetailsService, ILogger<UserPolicyDetailsController> logger)
        {
            _userPolicyDetailsService = userPolicyDetailsService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserPolicyDetailRequestModel requestModel)
        {
            await _userPolicyDetailsService.CreateItemAsync(requestModel);

            return Ok("registered Successfully");
        }

        // endpoint for HangFire
        [HttpPost ("HangFire")]
        public  IActionResult GenerateRecurringJob()
        {
            RecurringJob.AddOrUpdate<EmailService>("EmailService" , x=>x.GenerateEmail(), "* * * * * *");
            
            return  Ok();
        }
    }
}
