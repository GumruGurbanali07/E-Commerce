﻿using ECommerceAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services
{
	public interface IOrderService
	{
		Task CreateOrder(CreateOrder createOrder);
	}
}
