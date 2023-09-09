using System.Linq;
using System.Threading.Tasks;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Wallet;
using Mahya.Domain.ViewModels.Paging;
using Mahya.Domain.ViewModels.Wallet;
using Mahya.InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace Mahya.InfraData.Repository
{
    public class WalletRepository: IWalletRepository
    {
        #region constrator
        private readonly MahyaDbContext _context;
        public WalletRepository(MahyaDbContext context)
        {
            _context = context;
        }

        #endregion

        #region wallet

        public async Task CreateWallet(UserWallet userWallet)
        {
            await _context.UserWallets.AddAsync(userWallet);
        }

        public async Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter)
        {
            var query = _context.UserWallets.AsQueryable();

            #region filter
            if (filter.UserId != 0 && filter.UserId != null)
            {
                query = query.Where(c => c.UserId == filter.UserId);
            }
            #endregion

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).OrderByDescending(c=>c.CreateDate).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetWallets(allData);
        }

        public async Task<int> GetUserWalletAmount(long userId)
        {
            var variz = await _context.UserWallets.Where(c => c.UserId == userId && c.WalletType == WalletType.Variz && c.IsPay)
                .Select(c => c.Amount).ToListAsync();

            var bardasht = await _context.UserWallets.Where(c => c.UserId == userId && c.WalletType == WalletType.Bardasht && c.IsPay)
                .Select(c => c.Amount).ToListAsync();

            return (variz.Sum() - bardasht.Sum());
        }

        public async Task<UserWallet> GetUserWalletById(long walletId)
        {
            return await _context.UserWallets.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == walletId);
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateWallet(UserWallet wallet)
        {
            _context.UserWallets.Update(wallet);
        }
        #endregion
    }
}
