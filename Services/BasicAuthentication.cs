using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using xcart.Models;

namespace xcart.Services
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthenticationAttribute : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            try
            {
                if (actionContext.HttpContext.Request.Headers["Authorization"].Count != 0)
                {
                   
                    //Taking the parameter from the header  
                    var authToken1 = actionContext.HttpContext.Request.Headers["Authorization"].ToString();
                    var authToken = authToken1.Substring(5);
                    //decode the parameter  
                    var decoAuthToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                   
                    //split by colon : and store in variable  
                    var UserNameAndPassword = decoAuthToken.Split(':');
                    //Passing to a function for authorization  
                    var userService = actionContext.HttpContext.RequestServices.GetRequiredService<ILoginService>();
                    if (userService.VerifyUser(UserNameAndPassword[0], UserNameAndPassword[1]))
                    {
                        // setting current principle  
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserNameAndPassword[0]), null);
                    }
                    else
                    {
                        actionContext.Result= new UnauthorizedResult();
                    }
                }
                else
                {
                    actionContext.Result = new UnauthorizedResult();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

       


    }
}
