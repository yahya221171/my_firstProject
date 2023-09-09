using System;
using System.Threading.Tasks;
using Mahya.App.IServices;
using Mahya.Domain.ViewModels.Site.Sliders;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.Admin.Controllers
{
    public class SiteController : AdminBaseController
    {
        #region Constructor

        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        #endregion

        #region Slider

        #region FilterSlider

        public async Task<IActionResult> FilterSlider(FilterSlidersViewModel filter)
        {
            var data = await _siteService.FilterSlider(filter);
            return View(data);
        }

        #endregion

        #region CreateSlider

        [HttpGet]
        public async Task<IActionResult> CreateSlider()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSlider(CreateSliderViewModel create)
        {
            if (ModelState.IsValid)
            {
                var result = await _siteService.CreateSlider(create);
                switch (result)
                {
                    case CreateSliderResult.ImageNotFound:
                        TempData[ErrorMessage] = "چیزی یافت نشد";
                        break;
                    case CreateSliderResult.Success:
                        TempData[SuccessMessage] = "اسلایدر با موفقیت ثبت شد";

                        return RedirectToAction("FilterSlider");

                }
            }
            return View(create);
        }

        #endregion

        #region EditSlider
        [HttpGet]
        public async Task<IActionResult> EditSlider(long sliderId)
        {
            var data = await _siteService.GetEditSliderForShow(sliderId);
            if (data == null) return NotFound();
            return View(data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlider(EditSliderViewModel edit)
        {
            if (ModelState.IsValid)
            {
                var result = await _siteService.EditSlider(edit);
                switch (result)
                {
                    case EditSliderResult.NotFound:
                        TempData[ErrorMessage] = "خطایی در ویرایش رخ داده است";
                        break;
                    case EditSliderResult.Success:
                        TempData[SuccessMessage] = "ویرایش با موفقیت انجام شد";
                        return RedirectToAction("FilterSlider");
                }
            }
            return View(edit);
        }

        #endregion

        #region DeleteSlider

        public async Task<IActionResult> DeleteSlider(long sliderId)
        {
            await _siteService.DeleteSlider(sliderId);
            return RedirectToAction("FilterSlider");
        }

        #endregion
        #endregion

    }
}
