using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.Site;
using Mahya.Domain.ViewModels.Site.Sliders;

namespace Mahya.Domain.Interfaces
{
    public interface ISiteRepository
    {
        #region Slider

        public Task CreateSlider(Slider slider);
        public Task UpdateSlider(Slider slider);

        public Task SaveChanges();

        public Task<Slider>GetSliderById(long sliderId);
        public Task<FilterSlidersViewModel> FilterSlider(FilterSlidersViewModel filter);
        Task DeleteSlider(long sliderId);

        #endregion
    }
}
