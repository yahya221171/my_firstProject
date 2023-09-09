using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.App.Services;
using Mahya.Domain.Interfaces;
using Mahya.InfraData.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Mahya.IoC.DependencyContainer
{
    public class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<ISiteService,SiteService>();
            services.AddScoped<IOrderService,OrderService>();
        
            #endregion

            #region repositories
            services.AddScoped<IUserInterface, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            #endregion

            #region tools
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<IViewRenderService, RenderViewToString>();
            //services.AddScoped<Icaptch>();

            #endregion
        }
    }
}
