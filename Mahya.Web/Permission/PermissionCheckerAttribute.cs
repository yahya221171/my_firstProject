using Mahya.App.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mahya.Web.Permission
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        #region constractor
        private IUserService _userService;
        private long _permissionId = 0;
        public PermissionCheckerAttribute(long permissionId)
        {
            _permissionId = permissionId;
        }
        #endregion

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var email = context.HttpContext.User.Identity.Name;

                if (!_userService.CheckPermission(_permissionId, email))
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
