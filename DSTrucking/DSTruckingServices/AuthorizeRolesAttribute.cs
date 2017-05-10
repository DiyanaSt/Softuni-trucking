using DSTrucking.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace DSTrucking.Security
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
   
        private readonly string[] userAssignedRoles;
        
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = httpContext.User.Identity.GetUserId();
            if (userId != null)
            {
                foreach (var roles in userAssignedRoles)
                {
                    authorize = userManager.IsInRole(userId, roles);
                    if (authorize)
                        return authorize;
                }

                return authorize;
            }

            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/NotFound");
        }
    }
}