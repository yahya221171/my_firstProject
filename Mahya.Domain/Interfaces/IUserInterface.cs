using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.Account;
using Mahya.Domain.ViewModels.Account;
using Mahya.Domain.ViewModels.Admin.Account;

namespace Mahya.Domain.Interfaces
{
    public interface IUserInterface
    {
        #region Account
        Task<bool>IsExistEmail(string email);
        Task CreateUser(User user);
        Task SaveChanges();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(long userId);
        Task UpdateUser (User user);
        Task<bool> ActiveAccount(string activeCode);
        Task<User> GetUserByActiveCode(string activeCode);
        public bool CheckPermission(long permissionId, string email);
        Task<bool>IsExistProductFavorite(long productId,long userId);
        Task AddUserFavorite(UserFavorite userFavorite);
        void UpdateUserFavorite(UserFavorite userFavorite);
        Task<List<UserFavorite>>GetAllUserFavoriteForShow(long userId);
        Task<int> UserFavoriteCount(long userId);
        Task<bool> DeleteProductFavorite(long id);

        #endregion

        #region Admin-Side

        public Task<FilterUserViewModel> FilterUser(FilterUserViewModel filter);
        public Task<EditUserViewModel>EditUserForShow(long userId);
        public Task CreateRole(Role role);
        public void UpdateRole(Role role);
        public Task<Role> GetRoleById(long roleId);
        public Task<FilterRolesViewModel>FilterRoleViewModel(FilterRolesViewModel filter);
        public Task<CreateOrEditRoleViewModel>EditRoleForShow(long roleId);
        public Task<List<Permission>> GetAllPermission();
        public Task DeleteAllPermissionSelectedRole(long roleId);
        public Task AddAllPermissionSelectedRole(List<long> permissionList, long roleId);


        public Task<List<Role>> GetAllRole();
        public Task DeleteAllUesrSelectedRole(long userId);
        public Task AddAllUserSelectedRole(List<long> roList, long userId);

        #endregion
    }
}
