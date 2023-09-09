using System;
using System.Threading.Tasks;
using Mahya.App.IServices;
using Mahya.Domain.ViewModels.Admin.Account;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        #region Constructor

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region FilterUser

        public async Task<IActionResult> FilterUser(FilterUserViewModel filter)
        {
            var data = await _userService.FilterUser(filter);
            return View(data);
        }

        #endregion

        #region EditUserByAdmin
        [HttpGet]
        public async Task<IActionResult> EditUserByAdmin(long userId)
        {
            var user =await _userService.EditUserForShow(userId);
            if (user == null) return NotFound();
            ViewData["Roles"] = await _userService.GetAllRole();
            return View(user);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserByAdmin(EditUserViewModel edit)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditUserFromAdmin(edit);

                switch (result)
                {
                    case EditUserFromAdminResult.NotFound:
                        TempData[ErrorMessage] = "کاربری یافت نشد";
                        break;
                    case EditUserFromAdminResult.Success:
                        TempData[SuccessMessage] = "تغییرات با موفقیت انجام شد";

                        return RedirectToAction("FilterUser");
                }
            }
          
            return View(edit);
        }

        #endregion

        #region Filter-Create-Edit Role

        public async Task<IActionResult> FilterRole(FilterRolesViewModel filter)
        {
            var data = await _userService.FilterRole(filter);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateOrEditRoleViewModel create)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateOrEditRole(create);
                switch (result)
                {
                    case CreateOrEditRoleResult.NotFound:
                        TempData[ErrorMessage] = "نقشی یافت نشد";
                        break;
                    case CreateOrEditRoleResult.Success:
                        TempData[SuccessMessage] = "نقش جدید با موفقیت تعریف شد";
                        
                    //case CreateOrEditRoleResult.NotExistPermissions:
                    //    break;
                        return RedirectToAction("FilterRole");
                }
            }
            return View(create);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(long roleId)
        {
            ViewData["permission"] = await _userService.GetAllPermission();
            return View(await _userService.EditRoleForShow(roleId));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(CreateOrEditRoleViewModel edit)
        {
            ViewData["permission"] = await _userService.GetAllPermission();

            if (ModelState.IsValid)
            {
                var result = await _userService.CreateOrEditRole(edit);
                switch (result)
                {
                    case CreateOrEditRoleResult.NotFound:
                        TempData[ErrorMessage] = "نقشی یافت نشد";
                        break;
                    case CreateOrEditRoleResult.NotExistPermissions:
                        TempData[WarningMessage] = "لطفا دسترسی را انتخاب کنید";
                        break;
                    case CreateOrEditRoleResult.Success:
                        TempData[SuccessMessage] = "نقش با موفقیت تغییر یافت";


                        return RedirectToAction("FilterRole");
                }
            }
            return View(edit);
        }
        #endregion
    }
}
