using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.User.Controllers
{
    public class HomeController : UserBaseController1
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
