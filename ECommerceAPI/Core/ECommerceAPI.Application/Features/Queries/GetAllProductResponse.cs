using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries
{
	public class GetAllProductResponse
	{
		public int TotalCounts {  get; set; }
		public object Products { get; set; }
	}
}
