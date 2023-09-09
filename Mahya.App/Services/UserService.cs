
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.Extenstion.Convertors;
using Mahya.App.Extenstion.Generator;
using Mahya.App.Utils;
using Mahya.App.IServices;
using Mahya.App.Sender;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Account;
using Mahya.Domain.ViewModels.Account;
using Mahya.Domain.ViewModels.Admin.Account;
using Microsoft.AspNetCore.Http;

namespace Mahya.App.Services
{
    public class UserService:IUserService
    {
        #region Constructor

        private readonly IUserInterface _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IViewRenderService _renderService;

        public UserService(IUserInterface userRepository, IPasswordHelper passwordHelper, IViewRenderService renderService)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _renderService = renderService;
        }

        #endregion

        public async Task<RegisterUserResult> RegisterUse(RegisterUserViewModel register)
        {
            if (!await _userRepository.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                var user = new User()
                {
                    Email = FixedText.FixEmail(register.Email),
                    Avatar = "",
                    FirstName = register.FirstName,
                    IsBlocked = false,
                    IsDelete = false,
                    IsMobileActive = false,
                    LastName = register.LastName,
                    MobileActiveCode =NameGenerator.GenerateUniqCode(),
                    Password = _passwordHelper.EncodePasswordMd5(register.Password),
                    UserGender = UserGender.Unknown,
                    

                };
                await _userRepository.CreateUser(user);
                await _userRepository.SaveChanges();

               
                #region Send Activation Email

                string body = _renderService.RenderToStringAsync("_ActiveEmail", user);
                await SendEmail.Send(user.Email, "فعالسازی", body);

                #endregion
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.EmailExists;
        }

        public async Task<LoginUserResult> LoginUser(LoginUserViewModel login)
        {
            var user = await _userRepository.GetUserByEmail(FixedText.FixEmail(login.Email));
            if (user == null) return LoginUserResult.NotFound;
            if (!user.IsMobileActive) return LoginUserResult.NotActive;
            if (user.IsBlocked) return LoginUserResult.IsBlocked;
            if (user.Password != _passwordHelper.EncodePasswordMd5(login.Password)) return LoginUserResult.NotFound;

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        //public async Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel active)
        //{
        //    var user =await GetUserByEmail(FixedText.FixEmail(active.Email));
        //    if (user == null) return ActiveAccountResult.NotFound;
        //    if (user.MobileActiveCode == active.ActiveCode)
        //    {
               
        //        user.IsMobileActive = true;
        //        user.MobileActiveCode = NameGenerator.GenerateUniqCode();
        //        await _userRepository.UpdateUser(user);
        //        await _userRepository.SaveChanges();
        //        return ActiveAccountResult.Success;
        //    }

        //    return ActiveAccountResult.Error;

        //}

        public async Task<bool> ActiveAccount(string activeCode)
        {
            return await _userRepository.ActiveAccount(activeCode);
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (await _userRepository.IsExistEmail(FixedText.FixEmail(forgot.Email))){
                string fixedEmail = FixedText.FixEmail(forgot.Email);
            var user =await _userRepository.GetUserByEmail(fixedEmail);
            if (user == null) return ForgotPasswordResult.NotFound;

            string bodyEmail = _renderService.RenderToStringAsync("_ForgotPassword", user);
            await SendEmail.Send(user.Email, "بازیابی حساب کاربری", bodyEmail);
            return ForgotPasswordResult.Success;
            }

            return ForgotPasswordResult.EmailNotExist;
        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel reset)
        {
            var user =await _userRepository.GetUserByActiveCode(reset.MobileActiveCode);

            if (user == null)
            {
                return ResetPasswordResult.NotFound;
            }
            string hashNewPassword = _passwordHelper.EncodePasswordMd5(reset.Password);
            user.Password = hashNewPassword;
           await _userRepository.UpdateUser(user);
           await _userRepository.SaveChanges();
           return ResetPasswordResult.Success;
        }

        public bool CheckPermission(long permissionId, string email)
        {
            _userRepository.CheckPermission(permissionId, email);
            return true;
        }

        public async Task<bool> AddUserFavorite(long productId, long userId)
        {
            if (!await _userRepository.IsExistProductFavorite(productId, userId))
            {
                var userfavorit = new UserFavorite()
                {
                    UserId = userId,
                    ProductId = productId,
                    IsFavorite = true
                };
               await _userRepository.AddUserFavorite(userfavorit);
               await _userRepository.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<List<UserFavorite>> GetAllUserFavoriteForShow(long userId)
        {
            return await _userRepository.GetAllUserFavoriteForShow(userId);
        }

   

        public async Task<int> UserFavoriteCount(long userId)
        {
            return await _userRepository.UserFavoriteCount(userId);
        }

        public async Task<bool> DeleteProductFavorite(long id)
        {
           return await _userRepository.DeleteProductFavorite(id);
        }

        public async Task<EditUserProfileResult> EditUserProfile(long userId, IFormFile userAvatar, EditUserProfileViewModel editUserProfile)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return EditUserProfileResult.NotFound;
            user.FirstName = editUserProfile.FirstName;
            user.UserGender = editUserProfile.UserGender;
            user.LastName= editUserProfile.LastName;
            user.Address= editUserProfile.Address;
            user.PostAddress= editUserProfile.PostAddress;

            if (userAvatar != null && userAvatar.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(userAvatar.FileName);
                userAvatar.AddImageToServer(imageName, PathExtensions.UserAvatarOrginServer, 150, 150,
                    PathExtensions.UserAvatarThumbServer);
                user.Avatar = imageName;
            }

            await _userRepository.UpdateUser(user);
            await _userRepository.SaveChanges();
            return EditUserProfileResult.Success;
        }

        public async Task<EditUserProfileViewModel> GetEditUserProfileForShow(long userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return null;

            return new EditUserProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserGender = user.UserGender,
                Address = user.Address,
                PostAddress = user.PostAddress
            };
        }

        public async Task<ChangePasswordResult> ChangePassword(long userId, ChangePasswordViewModel changePassword)
        {
            var user= await _userRepository.GetUserById(userId);
            if (user != null)
            {
                var newPassword = _passwordHelper.EncodePasswordMd5(changePassword.NewPassword);
                if (user.Password != newPassword)
                {
                    user.Password= newPassword;
                    await _userRepository.UpdateUser(user);
                    await _userRepository.SaveChanges();
                    return ChangePasswordResult.Success;
                }

                return ChangePasswordResult.PasswordEqual;
            }

            return ChangePasswordResult.NotFound;
        }

        public async Task<FilterUserViewModel> FilterUser(FilterUserViewModel filter)
        {
            return await _userRepository.FilterUser(filter);
        }

        public async Task<EditUserViewModel> EditUserForShow(long userId)
        {
            return await _userRepository.EditUserForShow(userId);
        }

        public async Task<EditUserFromAdminResult> EditUserFromAdmin(EditUserViewModel edit)
        {
            var user = await _userRepository.GetUserById(edit.Id);
            if (user == null) return EditUserFromAdminResult.NotFound;
            //user.Email = edit.Email;
            user.FirstName = edit.FirstName;
            user.LastName = edit.LastName;
            if (!string.IsNullOrEmpty(edit.Password))
            {
                user.Password=edit.Password;
            }

            user.UserGender = edit.UserGender;
            await _userRepository.UpdateUser(user);
            await _userRepository.DeleteAllUesrSelectedRole(edit.Id);
            await _userRepository.AddAllUserSelectedRole(edit.RoleIds, edit.Id);
            await _userRepository.SaveChanges();
            return EditUserFromAdminResult.Success;
        }

        public async Task<FilterRolesViewModel> FilterRole(FilterRolesViewModel filter)
        {
            return await _userRepository.FilterRoleViewModel(filter);
        }

        public async Task<Role> GetRoleById(long roleId)
        {
            return await _userRepository.GetRoleById(roleId);
        }

        public async Task<CreateOrEditRoleResult> CreateOrEditRole(CreateOrEditRoleViewModel createOrEdit)
        {
            if (createOrEdit.Id != 0)
            {
                var role = await GetRoleById(createOrEdit.Id);
                if (role == null) return CreateOrEditRoleResult.NotFound;
                role.RoleTitle = createOrEdit.RoleTitle;
                _userRepository.UpdateRole(role);
                await _userRepository.DeleteAllPermissionSelectedRole(createOrEdit.Id);
                if (createOrEdit.SelectedPermission == null) return CreateOrEditRoleResult.NotExistPermissions;
                await _userRepository.AddAllPermissionSelectedRole(createOrEdit.SelectedPermission, createOrEdit.Id);
                await _userRepository.SaveChanges();
                return CreateOrEditRoleResult.Success;
            }

            else
            {
                var newRole = new Role()
                {
                    RoleTitle = createOrEdit.RoleTitle,
                };
                await _userRepository.CreateRole(newRole);
                if (createOrEdit.SelectedPermission == null) return CreateOrEditRoleResult.NotExistPermissions;

                await _userRepository.AddAllPermissionSelectedRole(createOrEdit.SelectedPermission, newRole.Id);

                await _userRepository.SaveChanges();
                return CreateOrEditRoleResult.Success;
            }

        }

        public async Task<CreateOrEditRoleViewModel> EditRoleForShow(long roleId)
        {
            return await _userRepository.EditRoleForShow(roleId);
        }

        public async Task<List<Permission>> GetAllPermission()
        {
            return await _userRepository.GetAllPermission();
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await _userRepository.GetAllRole();
        }

        public  Task DeleteAllUesrSelectedRole(long userId)
        {
            return  _userRepository.DeleteAllUesrSelectedRole(userId);
        }

        public  Task AddAllUserSelectedRole(List<long> roList, long userId)
        {
            return  _userRepository.AddAllUserSelectedRole(roList, userId);
        }
    }
}
