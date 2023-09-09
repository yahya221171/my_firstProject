using System.Collections.Generic;
using Mahya.Domain.Models.Site;
using Mahya.Domain.ViewModels.Paging;

namespace Mahya.Domain.ViewModels.Site.Sliders
{
    public class FilterSlidersViewModel:BasePaging
    {
        public string SliderTitle { get; set; }

        public List<Slider> Sliders { get; set; }

        #region methods
        public FilterSlidersViewModel SetSilders(List<Slider> sliders)
        {
            this.Sliders = sliders;
            return this;
        }

        public FilterSlidersViewModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntityCount = paging.AllEntityCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
            this.SkipEntitiy = paging.SkipEntitiy;
            this.PageCount = paging.PageCount;

            return this;
        }

        #endregion
    }
}
