using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Perficient.Training.JwtAuthentication
{
    public class LoginService : ILoginService
    {
        
        private readonly IConfiguration _config;

        public string GenerateJwt(User userInfo)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("challenge-JwtAuth"));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
    
            var claims = new[]{

                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email), 
                new Claim(ClaimTypes.Role, userInfo.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())   
            };
            
            var token = new JwtSecurityToken("JwtIssuer",    
            "JwtIssuer",    
            claims,    
            expires: DateTime.Now.AddMinutes(120),    
            signingCredentials: credentials);    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
            
        
        }
        

    }
}