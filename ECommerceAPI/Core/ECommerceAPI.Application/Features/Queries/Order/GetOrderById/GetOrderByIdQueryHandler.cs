﻿using ECommerceAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetOrderById
{
	
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
	{
		private readonly IOrderService _orderService;

		public GetOrderByIdQueryHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async  Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _orderService.GetOrderByIdAsync(request.Id);
			return new()
			{
				Id = data.Id,
				OrderCode = data.OrderCode,
				Address = data.Address,
				BasketItem = data.BasketItem,
				CreatedDate = data.CreatedDate,
				Description = data.Description,
				Completed = data.Completed
			};
		}
	}
}
