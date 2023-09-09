using System.Threading.Tasks;
using Mahya.Domain.Models.Wallet;
using Mahya.Domain.ViewModels.Wallet;

namespace Mahya.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task CreateWallet(UserWallet userWallet);
        Task<UserWallet> GetUserWalletById(long walletId);
        Task SaveChange();
        void UpdateWallet(UserWallet wallet);
        Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter);
        Task<int> GetUserWalletAmount(long userId);
    }
}
