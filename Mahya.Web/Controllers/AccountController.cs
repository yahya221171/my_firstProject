using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Mahya.App.Extenstion;
using Mahya.App.Extenstion.Convertors;
using Mahya.App.IServices;
using Mahya.App.Sender;
using Mahya.App.Services;
using Mahya.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly IViewRenderService _renderService;
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(IUserService userService, IViewRenderService renderService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _renderService = renderService;
            _captchaValidator = captchaValidator;
        }

        #endregion
        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            #region captcha Validator
            if (!await _captchaValidator.IsCaptchaPassedAsync(register.Token))
            {
                TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
                return View(register);
            }
            #endregion
            //var user= _userService.GetUserByEmail(register.Email);
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUse(register);
                switch (result)
                {
                    case RegisterUserResult.EmailExists:
                        TempData[ErrorMessage] = "ایمیل وارد شده قبلا ثبت نام کرده است";
                        break;
                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "ثبت نام انجام شد";
                        //#region Send Activation Email

                        //string body = _renderService.RenderToStringAsync("_ActiveEmail", user);
                        //await SendEmail.Send(user.Email, "فعالسازی", body);

                        //#endregion
                        //var user = await _userService.GetUserByEmail(User.Identity.Name);

                        return View("SuccessRegister");
                }
            }
            TempData[ErrorMessage] = "خطایی رخ داده مجدد تلاش کنید";
            return View(register);
        }
        #region login
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel login)
        {
            #region captcha Validator
            //if (!await _captchaValidator.IsCaptchaPassedAsync(login.Token))
            //{
            //    TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
            //    return View(login);
            //}
            #endregion
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(login);
                switch (result)
                {
                    case LoginUserResult.NotFound:
                        TempData[WarningMessage] = "کاربری یافت نشد";
                        break;
                    case LoginUserResult.NotActive:
                        TempData[ErrorMessage] = "حساب کاربری شما فعال نمیباشد";
                        break;
                    case LoginUserResult.IsBlocked:
                        TempData[WarningMessage] = "حساب شما توسط واحد پشتیبانی مسدود شده است";
                        TempData[InfoMessage] = "جهت اطلاع بیشتر لطفا به قسمت تماس باما مراجعه کنید";
                        break;
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByEmail(login.Email);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Email),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };
                        await HttpContext.SignInAsync(principle, properties);
                        TempData[SuccessMessage] = "شما با موفقیت وارد شدید";
                        return Redirect("/");
                }
            }

            return View(login);
        }
        #endregion

        #region log-out

        [HttpGet("log-Out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            TempData[InfoMessage] = "شما با موفقیت خارج شدید";
            return Redirect("/");
        }
        #endregion
        #region activate account

        [Route("account/activeaccount/{activeCode?}")]
        public async Task<IActionResult> ActiveAccount(string activeCode)
        {
            ViewBag.Activat = await _userService.ActiveAccount(activeCode);
            return View();
        }

        //[HttpPost("activate-account"), ValidateAntiForgeryToken]
        //public async Task<IActionResult> ActiveAccount(ActiveAccountViewModel activeAccount)
        //{

        //    #region captcha Validator
        //    if (!await _captchaValidator.IsCaptchaPassedAsync(activeAccount.Token))
        //    {
        //        TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
        //        return View(activeAccount);
        //    }
        //    #endregion
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userService.ActiveAccount(activeAccount);
        //        switch (result)
        //        {
        //            case ActiveAccountResult.Error:
        //                TempData[ErrorMessage] = "عملیات فعال کردن حساب کاربری با شکست مواجه شد";
        //                break;
        //            case ActiveAccountResult.NotFound:
        //                TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
        //                break;
        //            case ActiveAccountResult.Success:
        //                TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد";
        //                TempData[InfoMessage] = "لظفا جهت ادامه فراید وارد حساب کاربری خود شود";
        //                return RedirectToAction("Login");
        //        }
        //    }
        //    return View(activeAccount);
        //}

        #endregion


        #region Forgot Password
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            #region captcha Validator
            if (!await _captchaValidator.IsCaptchaPassedAsync(forgot.Token))
            {
                TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
                return View(forgot);
            }
            #endregion
            if (ModelState.IsValid)
            {
                var result = await _userService.ForgotPassword(forgot);

                switch (result)
                {
                    case ForgotPasswordResult.NotFound:
                        TempData[ErrorMessage] = "کاربری با این ایمیل یافت نشد";
                        break;
                    case ForgotPasswordResult.EmailNotExist:
                        TempData[ErrorMessage] = "کاربری با این ایمیل قبلا ثبت نام نکرده است";
                        break;
                    case ForgotPasswordResult.Success:
                        TempData[SuccessMessage] = "رمز عبور جدید برای شما ایمیل شد";
                        return Redirect("/");
                }
            }

            return View(forgot);
        }
        #endregion

        #region Reset Password
        [HttpGet("account/resetpassword/{code?}")]
        public async Task<IActionResult> ResetPassword(string code)
        {
            return View(new ResetPasswordViewModel()
            {
                MobileActiveCode = code
            });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel reset)
        {
            #region captcha Validator
            if (!await _captchaValidator.IsCaptchaPassedAsync(reset.Token))
            {
                TempData[ErrorMessage] = "کد کپچای شما معتبر نمیباشد";
                return View(reset);
            }
            #endregion
            if (ModelState.IsValid)
            {


                var result = await _userService.ResetPassword(reset);

                switch (result)
                {
                    case ResetPasswordResult.NotFound:
                        TempData[ErrorMessage] = "خطایی رخ داده است";
                        break;
                    case ResetPasswordResult.Success:
                        TempData[SuccessMessage] = "رمز عبور جدیدبا موفقیت تغییر یافت";

                        return RedirectToAction("Login");
                }

            }

            return View(reset);
        }
        #endregion
    }
}
