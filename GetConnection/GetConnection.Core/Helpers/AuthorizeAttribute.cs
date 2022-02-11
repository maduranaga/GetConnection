using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizesAttribute : Attribute, IAuthorizationFilter
{

    //private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;

   
    public AuthorizesAttribute( )
    {
       
       // _usersReadOnlyRepository = usersReadOnlyRepository;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        User res = new User();
            var user = context.HttpContext.User;
        //  var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // var x=_usersReadOnlyRepository.getById(5);

     
        if (user == null)
        {
  
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}

/*[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public  void OnAuthorization(AuthorizationFilterContext context)
    {

       // var x = await HttpContext.GetTokenAsync("Bearer", "access_token");


        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}*/
