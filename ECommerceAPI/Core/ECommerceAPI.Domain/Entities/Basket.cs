using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
	public class Basket: BaseEntity
	{
		public string UserId { get; set; } //IdentityUser<string>
		public AppUser User { get; set; }
		public ICollection<BasketItem> BasketItems { get; set; }
	}
}
