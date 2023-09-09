using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    [Route("User")]
    public class UserBaseController1 : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
        protected string InfoMessage = "InfoMessage";
    }
}
