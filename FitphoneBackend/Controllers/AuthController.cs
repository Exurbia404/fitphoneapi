using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FitphoneBackend.Business.Interfaces;
using FitphoneBackend.Controllers.DTOs;
using FitPhoneBackend.Business.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FitphoneBackend.API{
    [ApiController]
    [Route("/api/auth")]    
    public class AuthController:ControllerBase
    {
        private IAuthService _authService;
        
        public AuthController(IAuthService authservice){
            _authService = authservice;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUserAsync([FromBody] UserLoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                User userToLogin = await _authService.VerifyUserAsync(dto);
                
                return Ok($"Welcome,  {userToLogin.Username}");
            }
            catch
            {
                throw;
            }
        }
    }
}