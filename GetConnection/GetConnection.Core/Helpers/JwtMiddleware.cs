using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Helpers
{
   public class JwtMiddleware { 
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;



        public JwtMiddleware(IMapper mapper,RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
        _mapper = mapper;
    }

    public async Task Invoke(HttpContext context, IUsersReadOnlyRepository userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            attachUserToContext(context, userService, token);

        await _next(context);
    }

    private void attachUserToContext(HttpContext context, IUsersReadOnlyRepository userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("chamarafvbghythgefrvgtfdsaerrtgb");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                // attach user to context on successful jwt validation
                User res = new User();
                var x= userService.getById(userId);

                // var y=_mapper.Map<User, User>(x);

      



                context.Items["User"] = x.Result;
                




                
        }
        catch(Exception ex)
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }

        public bool RevokeToken(string token, AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"];

            var users = (User)context.HttpContext.Items["User"];

           /* var user = context.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

             return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

             return false if token is not active
            if (!refreshToken.IsActive) return false;

             revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _context.Update(user);
            _context.SaveChanges();*/

            return true;
        }

    }
}