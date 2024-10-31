using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.ViewModel.Basket
{
	public class UpdateBasketItemVM
	{
		public string BasketItemId {  get; set; }
		public int Quantity { get; set; }
	}
}
