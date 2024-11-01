using ECommerceAPI.Application.ViewModel.Basket;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services
{
	public interface IBasketService
	{
		public Task< List<BasketItem>> GetBasketItemAsync();
		public Task AddItemToBasketAsync(CreateBasketItemVM basketItem);
		public Task UpdateQuantityAsync(UpdateBasketItemVM updateBasketItem);
		public Task RemoveBasketItemAsync(string basketItemId);

	}
}
