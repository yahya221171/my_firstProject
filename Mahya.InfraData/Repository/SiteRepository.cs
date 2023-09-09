using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Site;
using Mahya.Domain.ViewModels.Paging;
using Mahya.Domain.ViewModels.Site.Sliders;
using Mahya.InfraData.Context;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Mahya.InfraData.Repository
{
    public class SiteRepository:ISiteRepository
    {
        #region Constructor

        private readonly MahyaDbContext _context;

        public SiteRepository(MahyaDbContext context)
        {
            _context = context;
        }

        #endregion


        public async Task CreateSlider(Slider slider)
        {
             await _context.Sliders.AddAsync(slider);
        }

        public async Task UpdateSlider(Slider slider)
        {
            _context.Sliders.Update(slider);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Slider> GetSliderById(long sliderId)
        {
            return await _context.Sliders.AsQueryable()
                .SingleOrDefaultAsync(s => s.Id == sliderId);
        }

        public async Task<FilterSlidersViewModel> FilterSlider(FilterSlidersViewModel filter)
        {
            var query = _context.Sliders.AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.SliderTitle))
            {
                query = query.Where(c => EF.Functions.Like(c.SliderTitle, $"%{filter.SliderTitle}%"));
            }

            #endregion

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetSilders(allData);
        }

        public async Task DeleteSlider(long sliderId)
        {
            var slider = await _context.Sliders.AsQueryable()
                .SingleOrDefaultAsync(s => s.Id == sliderId);

            if (slider != null)
            {
                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();
            }
        }
    }
}
