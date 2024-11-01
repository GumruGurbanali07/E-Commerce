﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Basket
{
	public class GetBasketItemQueryResponse
	{
		public string BasketItemId { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public int Quantity { get; set; }
	}
}
