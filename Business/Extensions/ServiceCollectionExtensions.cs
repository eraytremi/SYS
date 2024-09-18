using Business.Abstract;
using Business.Concrete;
using Business.HubService;
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

            services.AddSingleton<IStockMovementService, StockMovementService>();
            services.AddSingleton<IStockMovementRepository, StockMovementRepository>();

            services.AddSingleton<IStockRepository, StockRepository>();
            services.AddSingleton<IStockService, StockService>();

            services.AddSingleton<IWareHouseService, WareHouseService>();
            services.AddSingleton<IWareHouseRepository, WareHouseRepository>();

            services.AddSingleton<IEmailSender,MailSenderService>();

            services.AddSingleton<IDemandService, DemandService>();
            services.AddSingleton<IDemandRepository, DemandRepository>();

            services.AddSingleton<ISaleService, SalesService>();
            services.AddSingleton<ISalesRepository, SalesRepository>();

            services.AddSingleton<ISalesDetailsRepository, SalesDetailsRepository>();
            services.AddSingleton<ISaleDetailsService, SaleDetailsService>();          

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ICustomerService, CustomerService>();

            services.AddSingleton<IGroupMemberService, GroupMemberService>();
            services.AddSingleton<IGroupMemberRepository, GroupMemberRepository>();

            services.AddSingleton<IGroupRepository, GroupRepository>();
            services.AddSingleton<IGroupChatService, GroupChatService>();

            services.AddSingleton<IGroupMessageRepository, GroupMessageRepository>();
            services.AddSingleton<IGroupMessageService, GroupMessageService>();

            services.AddSingleton<IGroupMemberRepository, GroupMemberRepository>();
            services.AddSingleton<IGroupMemberService, GroupMemberService>();

            services.AddScoped<GroupMessageService>();
            services.AddTransient<ChatHub>();

            
        }
    }
}
