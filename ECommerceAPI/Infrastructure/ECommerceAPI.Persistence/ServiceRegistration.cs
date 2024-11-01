using Microsoft.Extensions.Configuration;
using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repository;
using ECommerceAPI.Persistence.Repository;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Persistence.Services;

namespace ECommerceAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
			services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
			services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
			services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
			services.AddScoped<IProductReadRepository, ProductReadRepository>();
			services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
			services.AddScoped<IOrderReadRepository, OrderReadRepository>();
			services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IBasketReadRepository, BasketReadRepository>();
			services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
			services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
			services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
			




		}
	}
}
