using GetConnection.Core.Entities;
using GetConnection.Core.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Services
{
    public class BearerTokenGenrate
    {
       private readonly AppSettings _appSettings;


     public BearerTokenGenrate(IOptions<AppSettings> appSettings)
     {
            _appSettings = appSettings.Value;

     }


        public string generateJwtToken(User user)
        {
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("chamarafvbghythgefrvgtfdsaerrtgb");

            var claims = new ClaimsIdentity(new Claim[]
                   {
                        new Claim("Id", user.Id.ToString())
                   });
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));


    


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,



                Expires = DateTime.UtcNow.AddDays(180),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

          

            var token = tokenHandler.CreateToken(tokenDescriptor);
           
            return tokenHandler.WriteToken(token);
        }
    }
}
