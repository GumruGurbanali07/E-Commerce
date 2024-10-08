using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Product.GetProductById
{
	public class GetProductRequest:IRequest<GetProductResponse>
	{
		public string Id { get; set; }
	}
}
