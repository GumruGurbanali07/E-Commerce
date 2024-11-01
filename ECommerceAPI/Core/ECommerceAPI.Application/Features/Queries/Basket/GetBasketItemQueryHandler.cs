using ECommerceAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Basket
{
	public class GetBasketItemQueryHandler : IRequestHandler<GetBasketItemQueryRequest, List<GetBasketItemQueryResponse>>
	{
		private readonly IBasketService _basketService;

		public GetBasketItemQueryHandler(IBasketService basketService)
		{
			_basketService = basketService;
		}
		public async Task<List<GetBasketItemQueryResponse>> Handle(GetBasketItemQueryRequest request, CancellationToken cancellationToken)
		{
			var basketItem = await _basketService.GetBasketItemAsync();
			return basketItem.Select(x => new GetBasketItemQueryResponse
			{
				BasketItemId = x.Id.ToString(),
				Name = x.Product.Name,
				Price = x.Product.Price,
				Quantity = x.Quantity
			}).ToList();
		}
	}
}
