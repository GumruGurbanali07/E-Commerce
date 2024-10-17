﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Product.GetProductById
{
	public class GetProductResponse
	{
		public string Name { get; set; }
		public int Stock { get; set; }
		public long Price { get; set; }
	}
}