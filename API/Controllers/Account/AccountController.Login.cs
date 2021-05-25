﻿using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Identity.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Account
{
    public partial class AccountController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            UserLoginRequest request, 
            CancellationToken cancellationToken)
        {
            var token = await _identityService.CreateToken(
                new CreateToken.Request
                {
                    Username = request.UserName,
                    Password = request.Password
                }, 
                cancellationToken);

            return Ok(token);
        }

        public class UserLoginRequest
        {
            [Required]
            public string UserName { get; set; }
            
            [Required]
            public string Password { get; set; }
        }
    }
}