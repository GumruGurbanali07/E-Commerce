﻿using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
	public class BasketItem : BaseEntity
	{
		public Guid ProductId { get; set; }
		public Product Product { get; set; }
		public Guid BasketId { get; set; }
		public Basket Basket { get; set; }

		public int Quantity { get; set; }
	}
}
