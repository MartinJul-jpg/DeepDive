using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeepDive.Filters
{
    public class AdminAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {

        public readonly string UserFilter;


        public AdminAuthorize (string userFilter)
        {
            UserFilter = userFilter;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.IsInRole("Admin"))
            {
                return;
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
             
        }
    }
}
