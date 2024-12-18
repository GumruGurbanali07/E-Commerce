﻿using ECommerceAPI.Application.Abstraction.Hubs;
using ECommerceAPI.SiganlR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.SiganlR.HubsServices
{
	public class ProductHubService : IProductHubService
	{
		private readonly IHubContext<ProductHub> _hubContext;

		public ProductHubService(IHubContext<ProductHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task ProductAddedMessageAsync(string message)
		{
			await _hubContext.Clients.All.SendAsync(ReceiveFunctionsName.ProductAddedMessage, message);

		}
	}
}
