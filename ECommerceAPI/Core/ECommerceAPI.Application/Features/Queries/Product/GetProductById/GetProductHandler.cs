using ECommerceAPI.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Product.GetProductById
{
	public class GetProductHandler : IRequestHandler<GetProductRequest, GetProductResponse>
	{
		private readonly IProductReadRepository _productReadRepository;

		public GetProductHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
		{
			ECommerceAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
			return new()
			{
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
			};
		}
	}
}
