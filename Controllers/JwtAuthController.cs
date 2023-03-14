using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Perficient.Training.JwtAuthentication
{


    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {

        /*
        private readonly IConfiguration _config;
            public JwtAuthController(IConfiguration config){
                _config = config;
            }


        private User AuthenticateUser(LoginDto login){
      

            UserService userService = new UserService();
            
            return userService.AuthenticateUser(login).Result;
        }
        
        private string GenerateJwt(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtAuth:Key"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
    
            var claims = new[]{

                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email), 
                new Claim(ClaimTypes.Role, userInfo.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())   
            };
            
            var token = new JwtSecurityToken(_config["JwtAuth:Issuer"],    
            _config["JwtAuth:Issuer"],    
            claims,    
            expires: DateTime.Now.AddMinutes(120),    
            signingCredentials: credentials);    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
            
        
        }
       */
        [AllowAnonymous]  
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto login){

            

            LoginService loginService = new LoginService();
            UserService userService = new UserService();


            var user = userService.AuthenticateUser(login).Result;
            
            
            if (user != null)
            {
                var tokenString = loginService.GenerateJwt(user);    
                return Ok(new { token = tokenString }); 
            }
            else
            {
                return Unauthorized();
            }
            
        }

    }
}
