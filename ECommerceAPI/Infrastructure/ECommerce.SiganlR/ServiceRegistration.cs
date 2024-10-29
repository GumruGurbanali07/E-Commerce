using ECommerceAPI.Application.Abstraction.Hubs;
using ECommerceAPI.SiganlR.HubsServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.SiganlR
{
	public static  class ServiceRegistration
	{
		public static void AddSignalRServices(this IServiceCollection collection)
		{
			collection.AddTransient<IProductHubService, ProductHubService>();
			collection.AddSignalR();
		}

	}
}
