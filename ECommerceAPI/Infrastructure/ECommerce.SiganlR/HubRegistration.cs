﻿using ECommerceAPI.Domain.Entities;
using ECommerceAPI.SiganlR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.SiganlR
{
	public static class HubRegistration
	{
		public static void MapHubs(this WebApplication webApplication)
		{
			webApplication.MapHub<ProductHub>("/product-hub");
			webApplication.MapHub<OrderHub>("/order-hub");
		}
	}
}
