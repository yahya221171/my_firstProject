using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.ViewModels.Site.Sliders;

namespace Mahya.App.IServices
{
    public interface ISiteService
    {
        #region Slider

        public Task<FilterSlidersViewModel> FilterSlider(FilterSlidersViewModel filter);
        public Task<CreateSliderResult> CreateSlider(CreateSliderViewModel create);
        public Task<EditSliderViewModel> GetEditSliderForShow(long sliderId);
        public Task<EditSliderResult> EditSlider(EditSliderViewModel edit);
        Task DeleteSlider(long sliderId);

        #endregion
    }
}
