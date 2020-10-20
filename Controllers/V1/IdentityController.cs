using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Register([FromBody] RegisterRequest request)
        {
            return Ok();
           
        }

       
    }
}
