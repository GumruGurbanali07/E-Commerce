using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order
{
	public class GetAllOrderQueryResponse
	{
        public string  Username { get; set; }
        public string  OrderCode { get; set; }
        public float  TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
