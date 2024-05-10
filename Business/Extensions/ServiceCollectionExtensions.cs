using Business.Abstract;
using Business.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository,UserRepository>();
            services.AddSingleton<IUserService,UserService>();

            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IProductService, ProductService>();

            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICategoryService, CategoryService>();

            services.AddSingleton<IUserRoleRepository, UserRoleRepository>();
            services.AddSingleton<IUserRoleService, UserRoleService>();

            services.AddSingleton<IRoleRepository, RoleRepository>();
            services.AddSingleton<IRoleService, RoleService>();

            services.AddSingleton<ISupplierRepository, SupplierRepository>();
            services.AddSingleton<ISupplierService, SupplierService>();


        }
    }
}
