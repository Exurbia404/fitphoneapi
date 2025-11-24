using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Controllers.DTOs;
using FitPhoneBackend.Business.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using FitPhoneBackend.Business.Services;
using FitPhoneBackend.API.Utils;

namespace FitphoneBackend.API{
    [ApiController]
    [Route("/api/auth")]    
    public class AuthController:ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authservice)
        {
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
                
                User sanitizedUser = UserUtils.SanitizeUser(userToLogin); //getting rid of sensitive info on login

                string jwtToken = JWTHandler.GenerateJWTToken(sanitizedUser);

                Response.Cookies.Append("access_token", jwtToken, new CookieOptions
                {
                    HttpOnly = true,                     //JS cannot access it
                    Secure = true,                       //only over HTTPS
                    SameSite = SameSiteMode.Strict,      //prevent CSRF
                    Expires = DateTime.UtcNow.AddMinutes(60) //short lifetime
                });
                
                return Ok(new
                {
                    message = $"Welcome,  {sanitizedUser.Username}",
                    user = sanitizedUser
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}