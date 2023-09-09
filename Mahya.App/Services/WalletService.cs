﻿using System.Threading.Tasks;
using Mahya.App.IServices;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Wallet;
using Mahya.Domain.ViewModels.Wallet;

namespace Mahya.App.Services
{
    public class WalletService : IWalletService
    {
        #region constractor
        private readonly IUserInterface _userRepository;
        private readonly IWalletRepository _walletRepository;
        public WalletService(IUserInterface userRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        #endregion

        public async Task<long> ChargeWallet(long userId, ChargeWalletViewModel chargeWallet, string description)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return 0;

            var wallet = new UserWallet
            {
                UserId = userId,
                Amount = chargeWallet.Amount,
                Description = description,
                IsPay = false,
                WalletType = WalletType.Variz
            };
            await _walletRepository.CreateWallet(wallet);
            await _walletRepository.SaveChange();

            return wallet.Id;
        }

        public async Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter)
        {
            return await _walletRepository.FilterWallets(filter);
        }

        public async Task<int> GetUserWalletAmount(long userId)
        {
            return await _walletRepository.GetUserWalletAmount(userId);
        }

        public async Task<UserWallet> GetUserWalletById(long walletId)
        {
            return await _walletRepository.GetUserWalletById(walletId);
        }

        public async Task<bool> UpdateWalletForCharge(UserWallet wallet)
        {
            if (wallet != null)
            {
                wallet.IsPay = true;
                _walletRepository.UpdateWallet(wallet);
                await _walletRepository.SaveChange();
                return true;
            }
            return false;
        }
    }
}
