using ECommerceAPI.Application.Repository;
using ECommerceAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries
{
	public class GetAllProductHandler : IRequestHandler<GetAllProductRequest, GetAllProductResponse>
	{
		private readonly IProductReadRepository _productReadRepository;

		public GetAllProductHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<GetAllProductResponse> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
		{
			var totalCount = _productReadRepository.GetAll(false).Count();
			var products = _productReadRepository.GetAll(false)
				.Skip(request.Page * request.Size)
				.Take(request.Size)
				.Select(x => new
				{
					x.Id,
					x.Name,
					x.Price,
					x.Stock,
					x.CreatedDate,
					x.UpdateDate,
				}).ToList();

			return new()
			{
				Products = products,
				TotalCounts = totalCount
			};
		}
	}
}
