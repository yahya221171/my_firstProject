using System.Threading.Tasks;
using Mahya.Domain.Models.Wallet;
using Mahya.Domain.ViewModels.Wallet;

namespace Mahya.App.IServices
{
    public interface IWalletService
    {
        Task<long> ChargeWallet(long userId, ChargeWalletViewModel chargeWallet, string description);
        Task<UserWallet> GetUserWalletById(long walletId);
        Task<bool> UpdateWalletForCharge(UserWallet wallet);
        Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter);
        Task<int> GetUserWalletAmount(long userId);

    }
}
