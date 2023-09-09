using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.Account;
using Mahya.Domain.Models.Orders;
using Mahya.Domain.Models.ProductEntity;
using Mahya.Domain.Models.Site;
using Mahya.Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models.Account;

namespace Mahya.InfraData.Context
{
    public class MahyaDbContext:DbContext
    {
        #region constractor
        public MahyaDbContext(DbContextOptions<MahyaDbContext> options) : base(options)
        {

        }
        #endregion

        #region user
        public DbSet<User> Users { get; set; }
        public DbSet<UserWallet> UserWallets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        #endregion

        #region Product

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSelectedCategories> ProductSelectedCategories { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductGalleries> ProductGalleries { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        #endregion

        #region Order-Detail

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }



        #endregion

        #region DefaultQuery

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<UserRole>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<UserWallet>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Permission>()
                .HasQueryFilter(g => !g.IsDelete);
            modelBuilder.Entity<RolePermission>()
                .HasQueryFilter(u => !u.IsDelete);
            //modelBuilder.Entity<Product>()
            //    .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<ProductComment>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<ProductFeature>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<ProductSelectedCategories>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<ProductCategory>()
                .HasQueryFilter(u => !u.IsDelete);



            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
