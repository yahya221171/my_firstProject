using System;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.Domain.ViewModels.Account;
using Mahya.Domain.ViewModels.Admin.Order;
using Mahya.Domain.ViewModels.Wallet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ZarinpalSandbox;

namespace Mahya.Web.Areas.User.Controllers
{
    public class AccountController : UserBaseController1
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly IWalletService _walletService;
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;

        public AccountController(IUserService userService, IWalletService walletService, IConfiguration configuration, IOrderService orderService)
        {
            _userService = userService;
            _walletService = walletService;
            _configuration = configuration;
            _orderService = orderService;
        }

        #endregion

        #region EditUserProfil

        [HttpGet("Edit-User-Profile")]
        public async Task<IActionResult> EditUserProfile()
        {
            var user = await _userService.GetEditUserProfileForShow(User.GetUserId());
            if (user == null) return NotFound();
            return View(user);
        }
        [HttpPost("Edit-User-Profile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel edit, IFormFile userAvatar)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditUserProfile(User.GetUserId(), userAvatar, edit);
                switch (result)
                {
                    case EditUserProfileResult.NotFound:
                        TempData[ErrorMessage] = "کاربری یافت نشد";
                        break;
                    case EditUserProfileResult.Success:
                        TempData[SuccessMessage] = "تغییرات با موفقیت انجام شد";
                        return RedirectToAction("EditUserProfile");
                }

            }
            return View(edit);
        }
        #endregion

        #region ChangePassword
        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost("change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangePassword(User.GetUserId(), changePassword);
                switch (result)
                {
                    case ChangePasswordResult.NotFound:
                        TempData[ErrorMessage] = "کاربری یافت نشد ";
                        break;
                    case ChangePasswordResult.PasswordEqual:
                        TempData[ErrorMessage] = "رمز عبور وارد شده با رمز عبور قبلی برابر است";
                        ModelState.AddModelError("NewPassword", "رمز عبور وارد شده با رمز عبور قبلی برابر است");
                        break;
                    case ChangePasswordResult.Success:
                        TempData[SuccessMessage] = "رمز عبور با موفقیت تغییر یافت";
                        TempData[InfoMessage] = "لطفا برای تکمیل فرآیند مجددا وارد شوید";
                        await HttpContext.SignOutAsync();
                        return RedirectToAction("Login", "Account", new { area = "" });

                }
            }
            return View(changePassword);
        }
        #endregion

        #region ChargeWallet
        [HttpGet("charge-wallet")]
        public async Task<IActionResult> ChargeWallet()
        {
            return View();
        }
        [HttpPost("charge-wallet"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel chargeWallet)
        {
            if (ModelState.IsValid)
            {
                var walletId = await _walletService.ChargeWallet(User.GetUserId(), chargeWallet,
                    $"واریز به مبلغ{chargeWallet.Amount}");

                #region Payment

                var payment = new Payment(chargeWallet.Amount);
                var url = _configuration.GetSection("DefaultURL")["Host"] + "/user/online-payment/" + walletId;
                var result = payment.PaymentRequest("شارژ گیف پول", url);
                if (result.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
                }
                else
                {
                    TempData[ErrorMessage] = "مشکلی در پرداخت به وجود آماده است،لطفا مجددا امتحان کنید";
                }
                #endregion
            }
            return View(chargeWallet);
        }
        #endregion

        #region online payment
        [HttpGet("online-payment/{id}")]
        public async Task<IActionResult> OnlinePayment(long id)
        {
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = await _walletService.GetUserWalletById(id);
                if (wallet != null)
                {
                    var payment = new Payment(wallet.Amount);
                    var result = payment.Verification(authority).Result;

                    if (result.Status == 100)
                    {
                        ViewBag.RefId = result.RefId;
                        ViewBag.Success = true;
                        await _walletService.UpdateWalletForCharge(wallet);
                    }
                    return View();
                }
                return NotFound();
            }
            return View();
        }
        #endregion

        #region UserWallet

        [HttpGet("user-wallet")]
        public async Task<IActionResult> UserWallet(FilterWalletViewModel filter)
        {
            filter.UserId = User.GetUserId();
            filter.TakeEntity = 10;
            return View(await _walletService.FilterWallets(filter));
        }

        #endregion

        #region UserBasket
        [HttpGet("basket/{orderId}")]
        public async Task<IActionResult> UserBasket(long orderId)
        {
            var data = await _orderService.GetUserBasket(User.GetUserId(), orderId);

            if (data == null) return NotFound();
            ViewBag.userWalletAmount = await _walletService.GetUserWalletAmount(User.GetUserId());
            ViewBag.userInfo = await _userService.GetUserById(User.GetUserId());
            return View(data);
        }
        [HttpPost("basket/{orderId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UserBasket(FinallyOrderViewModel finallyOrder)
        {
            if (finallyOrder.PayWithWallet)
            {
                var result1 = await _orderService.FinallyOrder(finallyOrder, User.GetUserId());
                switch (result1)
                {
                    case FinallyOrderResult.HasNotUser:
                        TempData[WarningMessage] = "کاربری یافت نشد";
                        break;
                    case FinallyOrderResult.NotFound:
                        TempData[ErrorMessage] = "سفارشی یافت نشد";
                        break;
                    case FinallyOrderResult.Error:
                        TempData[ErrorMessage] = "موجودی کیف پول شما کافی نیست";
                        return RedirectToAction("UserWallet");

                    case FinallyOrderResult.Suceess:
                        TempData[SuccessMessage] = "پرداخت با موفقیت انجام شد";
                        return RedirectToAction("UserWallet");
                }
            }
            else
            {
                var order = await _orderService.GetOrderById(finallyOrder.OrderId);
                #region Payment

                var payment = new Payment(order.OrderSum);
                var url = _configuration.GetSection("DefaultURL")["Host"] + "/user/online-payment-order/" + order.Id;
                var result = payment.PaymentRequest("خرید آنلاین", url);
                if (result.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
                }
                else
                {
                    TempData[ErrorMessage] = "مشکلی در پرداخت به وجود آماده است،لطفا مجددا امتحان کنید";
                }
                #endregion 
            }
            ViewBag.userWalletAmount = await _walletService.GetUserWalletAmount(User.GetUserId());
            ViewBag.userInfo = await _userService.GetUserById(User.GetUserId());
            return View();
        }

        #endregion

        #region DeleteOrderDetail
        [HttpGet("delete-orderDetail/{detailId}")]
        public async Task<IActionResult> DeleteOrderDetail(long detailId)
        {

            var result = await _orderService.DeleteOrderDetail(detailId);
            if (result)
            {
                //return Redirect("/");
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.Error();

        }

        #endregion

        #region online payment-order
        [HttpGet("online-payment-order/{id}")]
        public async Task<IActionResult> OnlinePaymentOrder(long id)
        {
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var order = await _orderService.GetOrderById(id);
                if (order != null)
                {
                    var payment = new Payment(order.OrderSum);
                    var result = payment.Verification(authority).Result;

                    if (result.Status == 100)
                    {
                        ViewBag.RefId = result.RefId;
                        ViewBag.Success = true;
                        await _orderService.PayOnlineInFinallyOrder(order.Id);
                    }
                    return View();
                }
                return NotFound();
            }
            return View();
        }
        #endregion

        #region UserOrder
        [HttpGet("user-order")]
        public async Task<IActionResult> UserOrder(FilterOrdersViewModel filter)
        {
            filter.UserId = User.GetUserId();
            return View(await _orderService.FilterOrders(filter));
        }

        #endregion

        #region Favorite
        [HttpGet("user-favorite/{productId}")]
        public async Task<IActionResult> AddUserFavorite(long productId)
        {
            var result = await _userService.AddUserFavorite(productId, User.GetUserId());
            if (result)
            {
                TempData[SuccessMessage] = "محصول با موفقیت به علاقه مندی ها اضافه شد";
                return RedirectToAction("AllUserFavorite");
            }
            TempData[WarningMessage] = "محصول قبلا یه علاقه مندی ها اضافه شده بود";
            return Redirect("/");
        }
        [HttpGet("all-favorite")]
        public async Task<IActionResult> AllUserFavorite()
        {
            var data = await _userService.GetAllUserFavoriteForShow(User.GetUserId());
            if (data == null) return NotFound();
            return View(data);
        }

        [HttpGet("delete-favorite/{id}")]
        public async Task<IActionResult> RemoveProductFavorite(long id)
        {
            var result = await _userService.DeleteProductFavorite(id);
            if (result)
            {
                TempData[SuccessMessage] = "محصول شما با موفقیت حذف شد";
                return RedirectToAction("AllUserFavorite");
            }
            TempData[WarningMessage] = "حطا در حذف محصول";
            return Redirect("/");
        }
       
        #endregion
    }
}
