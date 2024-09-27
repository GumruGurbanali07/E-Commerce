using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.ViewModel.Product
{
	public class CreateProductVM
	{
		public string Name { get; set; }
		public int Stock { get; set; }
		public long Price { get; set; }
	}
}
