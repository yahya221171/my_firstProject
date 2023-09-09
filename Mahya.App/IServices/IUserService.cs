using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.Account;
using Mahya.Domain.ViewModels.Account;
using Mahya.Domain.ViewModels.Admin.Account;
using Microsoft.AspNetCore.Http;

namespace Mahya.App.IServices
{
    public interface IUserService
    {
        #region Account

        Task<RegisterUserResult> RegisterUse(RegisterUserViewModel register);
        Task<LoginUserResult> LoginUser(LoginUserViewModel login);
        Task<User> GetUserByEmail(string email);
        //Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel active);
        Task<bool> ActiveAccount(string activeCode);
        Task<User> GetUserById(long userId);
        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel forgot);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel reset);
        public bool CheckPermission(long permissionId, string email);
        Task<bool>AddUserFavorite(long productId,long userId);
        Task<List<UserFavorite>> GetAllUserFavoriteForShow(long userId);
        Task<int> UserFavoriteCount(long userId);
        Task<bool> DeleteProductFavorite(long id);


        #endregion

        #region Profil

        public Task<EditUserProfileResult> EditUserProfile(long userId, IFormFile userAvatar,
            EditUserProfileViewModel editUserProfile);

        public Task<EditUserProfileViewModel> GetEditUserProfileForShow(long userId);
        public Task<ChangePasswordResult> ChangePassword(long userId, ChangePasswordViewModel changePassword);

        #endregion

        #region Admin-Side

        public Task<FilterUserViewModel> FilterUser(FilterUserViewModel filter);
        public Task<EditUserViewModel> EditUserForShow(long userId);
        public Task<EditUserFromAdminResult> EditUserFromAdmin(EditUserViewModel edit);
        public Task<FilterRolesViewModel> FilterRole(FilterRolesViewModel filter);
        public Task<Role> GetRoleById(long roleId);
        public Task<CreateOrEditRoleResult> CreateOrEditRole(CreateOrEditRoleViewModel createOrEdit);
        public Task<CreateOrEditRoleViewModel> EditRoleForShow(long roleId);
        public Task<List<Permission>> GetAllPermission();

        public Task<List<Role>> GetAllRole();
        public Task DeleteAllUesrSelectedRole(long userId);
        public Task AddAllUserSelectedRole(List<long> roList, long userId);

        #endregion
    }
}
