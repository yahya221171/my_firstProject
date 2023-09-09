using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.App.Utils;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Site;
using Mahya.Domain.ViewModels.Site.Sliders;

namespace Mahya.App.Services
{
    public class SiteService : ISiteService
    {
        #region Constructor

        private readonly ISiteRepository _siteRepository;

        public SiteService(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        #endregion


        public async Task<FilterSlidersViewModel> FilterSlider(FilterSlidersViewModel filter)
        {
            return await _siteRepository.FilterSlider(filter);
        }

        public async Task<CreateSliderResult> CreateSlider(CreateSliderViewModel create)
        {
            var slider = new Slider()
            {
                Href = create.Href,
                Price = create.Price,
                SliderText = create.SliderText,
                SliderTitle = create.SliderTitle,
                TextBtn = create.TextBtn
            };
            if (create.ImageFile != null && create.ImageFile.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(create.ImageFile.FileName);
                create.ImageFile.AddImageToServer(imageName, PathExtensions.SliderOrginServer, 150, 150, PathExtensions.SliderThumbServer
                    );
                slider.SliderImage = imageName;
            }
            else
            {
              return  CreateSliderResult.ImageNotFound;
            }

            await _siteRepository.CreateSlider(slider);
            await _siteRepository.SaveChanges();
            return CreateSliderResult.Success;
        }

        public async Task<EditSliderViewModel> GetEditSliderForShow(long sliderId)
        {
            var currentSlider = await _siteRepository.GetSliderById(sliderId);
            if (currentSlider != null)
            {
                return new EditSliderViewModel()
                {
                    Price = currentSlider.Price,
                    Href = currentSlider.Href,
                    SliderText = currentSlider.SliderText,
                    SliderTitle = currentSlider.SliderTitle,
                    TextBtn = currentSlider.TextBtn,
                    SliderImage = currentSlider.SliderImage
                };

            }
            return null;
        }

        public async Task<EditSliderResult> EditSlider(EditSliderViewModel edit)
        {
            var currentSlider = await _siteRepository.GetSliderById(edit.SliderId);
            if (currentSlider == null) return EditSliderResult.NotFound;
            currentSlider.Price = edit.Price;
            currentSlider.Href = edit.Href;
            currentSlider.SliderText = edit.SliderText;
            currentSlider.SliderTitle = edit.SliderTitle;
            currentSlider.TextBtn = edit.TextBtn;
            if (edit.ImageFile != null && edit.ImageFile.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(edit.ImageFile.FileName);
                edit.ImageFile.AddImageToServer(imageName, PathExtensions.SliderOrginServer, 1920, 600, PathExtensions.SliderThumbServer,
                currentSlider.SliderImage);
                currentSlider.SliderImage = imageName;
            }

            await _siteRepository.UpdateSlider(currentSlider);
            await _siteRepository.SaveChanges();
            return EditSliderResult.Success;
        }

        public async Task DeleteSlider(long sliderId)
        {
             await _siteRepository.DeleteSlider(sliderId);
        }
    }

}
