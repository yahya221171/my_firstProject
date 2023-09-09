using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.App.Extenstion.Generator;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Account;
using Mahya.Domain.ViewModels.Account;
using Mahya.Domain.ViewModels.Admin.Account;
using Mahya.Domain.ViewModels.Paging;
using Mahya.InfraData.Context;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models.Account;

namespace Mahya.InfraData.Repository
{
    public class UserRepository:IUserInterface
    {
        #region Constructor

        private readonly MahyaDbContext _context;

        public UserRepository(MahyaDbContext context)
        {
            _context = context;
        }

        #endregion


        public async Task<bool> IsExistEmail(string email)
        {
            return await _context.Users.AnyAsync(c => c.Email == email);
        }

        public async Task CreateUser(User user)
        {
             await _context.Users.AddAsync(user);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.AsQueryable()
                .SingleOrDefaultAsync(c => c.Email == email);
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _context.Users.SingleOrDefaultAsync(c => c.Id == userId);
        }

        public async Task UpdateUser(User user)
        {
             _context.Users.Update(user);
        }

        public async Task<bool> ActiveAccount(string activeCode)
        {
            var user = await _context.Users.SingleOrDefaultAsync(c => c.MobileActiveCode == activeCode);
            if (user == null || user.IsMobileActive) return false;

            user.IsMobileActive = true;
            user.MobileActiveCode = NameGenerator.GenerateUniqCode();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _context.Users.SingleOrDefaultAsync(c => c.MobileActiveCode == activeCode);

        }

        public bool CheckPermission(long permissionId, string email)
        {
            long userId = _context.Users.AsQueryable().Single(c => c.Email == email).Id;

            var userRole = _context.UserRoles.AsQueryable()
                .Where(c => c.UserId == userId).Select(r => r.RoleId).ToList();


            if (!userRole.Any())
                return false;


            var permissions = _context.RolePermissions.AsQueryable()
                .Where(c => c.PermissionId == permissionId).Select(c => c.RoleId).ToList();


            return permissions.Any(c => userRole.Contains(c));
        }

        public async Task<bool> IsExistProductFavorite(long productId, long userId)
        {
            return await _context.UserFavorites.AsQueryable()
                .AnyAsync(c => c.UserId == userId && c.ProductId == productId && !c.IsDelete);
        }

        public async Task AddUserFavorite(UserFavorite userFavorite)
        {
            await _context.UserFavorites.AddAsync(userFavorite);
        }

     

        public void UpdateUserFavorite(UserFavorite userFavorite)
        {
            _context.UserFavorites.Update(userFavorite);
        }

        public async Task<List<UserFavorite>> GetAllUserFavoriteForShow(long userId)
        {
            return await _context.UserFavorites.AsQueryable()
                .Include(c=>c.Product)
                .Include(c=>c.User)
                .ThenInclude(c=>c.Orders)
                .Where(c => c.UserId == userId&&!c.IsDelete)
                .ToListAsync();
        }

        public async Task<int> UserFavoriteCount(long userId)
        {
            return await _context.UserFavorites.AsQueryable()
                .Where(c => c.UserId == userId)
                .CountAsync();
        }

        public async Task<bool> DeleteProductFavorite(long id)
        {
            var currentFavorite = await _context.UserFavorites.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == id);
            if (currentFavorite != null)
            {
                _context.Remove(currentFavorite);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<FilterUserViewModel> FilterUser(FilterUserViewModel filter)
        {

            var query = _context.Users.AsQueryable();


            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(c => c.Email == filter.Email);
            }


            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetUsers(allData);

        }

        public async Task<EditUserViewModel> EditUserForShow(long userId)
        {
            return await _context.Users.AsQueryable()
                .Where(c => c.Id == userId)
                .Select(c => new EditUserViewModel()
                {Id = c.Id,
                    Email = c.Email,
                    Password = c.Password,
                    UserGender = c.UserGender,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    PostAddress = c.PostAddress,
                    RoleIds = c.UserRoles.Where(s=>s.UserId==userId).Select(g=>g.RoleId).ToList(),

                }).FirstOrDefaultAsync();
        }

        public async Task CreateRole(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }

        public async Task<Role> GetRoleById(long roleId)
        {
            return await _context.Roles.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == roleId);
        }

        public async Task<FilterRolesViewModel> FilterRoleViewModel(FilterRolesViewModel filter)
        {
            var query = _context.Roles.AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.RoleName))
            {
                query = query.Where(c => EF.Functions.Like(c.RoleTitle, $"%{filter.RoleName}%"));
            }

            #endregion

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetRoles(allData);
        }

        public async Task<CreateOrEditRoleViewModel> EditRoleForShow(long roleId)
        {
            return await _context.Roles.AsQueryable()
                .Include(c=>c.RolePermissions)
                .Where(c => c.Id == roleId)
                .Select(c => new CreateOrEditRoleViewModel()
                {
                    Id = c.Id,
                    RoleTitle = c.RoleTitle,
                    SelectedPermission = c.RolePermissions.Select(x=>x.PermissionId).ToList()
                })
                .SingleOrDefaultAsync();
        }

        public async Task<List<Permission>> GetAllPermission()
        {
            return await _context.Permissions.AsQueryable().ToListAsync();
        }

        public async Task DeleteAllPermissionSelectedRole(long roleId)
        {
            var allRolePermission = await _context.RolePermissions
                .Where(c => c.RoleId == roleId)
                .ToListAsync();
            if (allRolePermission.Any())
            {
                _context.RolePermissions.RemoveRange(allRolePermission);
            }

        }

        public async Task AddAllPermissionSelectedRole(List<long> permissionList, long roleId)
        {
            if (permissionList != null && permissionList.Any())
            {
                var rolePermissions = new List<RolePermission>();
                foreach (var permissionId in permissionList)
                {
                    rolePermissions.Add(new RolePermission()
                    {
                        PermissionId = permissionId,
                        RoleId = roleId
                    });
                }

                await _context.RolePermissions.AddRangeAsync(rolePermissions);
            } 
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await _context.Roles.AsQueryable().ToListAsync();
        }

        public async Task DeleteAllUesrSelectedRole(long userId)
        {
            var allUserRole = await _context.UserRoles
                .Where(c => c.UserId == userId)
                .ToListAsync();
            if (allUserRole.Any())
            {
                _context.UserRoles.RemoveRange(allUserRole);
            }
        }

        public async Task AddAllUserSelectedRole(List<long> roList, long userId)
        {
            if (roList != null && roList.Any())
            {
                var userRole = new List<UserRole>();
                foreach (var roleId in roList)
                {
                    userRole.Add(new UserRole()
                    {
                        RoleId = roleId,
                        UserId = userId
                    });
                }

                await _context.UserRoles.AddRangeAsync(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
