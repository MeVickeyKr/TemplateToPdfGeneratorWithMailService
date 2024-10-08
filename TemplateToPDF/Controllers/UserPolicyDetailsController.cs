﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateToPDF.Services.ApiResquestModel;
using TemplateToPDF.Services.Interface;

namespace TemplateToPDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPolicyDetailsController : ControllerBase
    {
        private readonly IUserPolicyDetailsService _userPolicyDetailsService;
        public UserPolicyDetailsController(IUserPolicyDetailsService userPolicyDetailsService)
        {
            _userPolicyDetailsService = userPolicyDetailsService;
        }

        [HttpPost]
        public async Task<ActionResult<UserPolicyDetailRequestModel>> PostAsync([FromBody] UserPolicyDetailRequestModel requestModel)
        {
            await _userPolicyDetailsService.CreateItemAsync(requestModel);

            return Ok(requestModel);
        }
    }
}
