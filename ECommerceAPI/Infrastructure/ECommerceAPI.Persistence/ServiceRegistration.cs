using Microsoft.Extensions.Configuration;
using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
	public static  class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
		}
	}
}
