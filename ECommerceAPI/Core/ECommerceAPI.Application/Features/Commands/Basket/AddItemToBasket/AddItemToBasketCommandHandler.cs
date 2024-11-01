﻿using ECommerceAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Basket.AddItemToBasket
{
	public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
	{
		private readonly IBasketService _basketService;

		public AddItemToBasketCommandHandler(IBasketService basketService)
		{
			_basketService = basketService;
		}

		public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
		{
			await _basketService.AddItemToBasketAsync(new()
			{
				ProductId = request.ProductId,
				Quantity = request.Quantity,
			});
			return new();
		}
	}
}
