﻿using Mahya.Web.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    [PermissionChecker(1)]
    public class AdminBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
        protected string InfoMessage = "InfoMessage";
    }
}
