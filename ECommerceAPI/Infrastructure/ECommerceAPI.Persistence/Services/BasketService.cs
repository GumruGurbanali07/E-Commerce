using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.ViewModel.Basket;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class BasketService : IBasketService
	{
		public Task AddItemToBasketAsync(CreateBasketItemVM basketItem)
		{
			throw new NotImplementedException();
		}

		public Task<List<BasketItem>> GetBasketItemAsync()
		{
			throw new NotImplementedException();
		}

		public Task RemoveBasketItemAsync(string basketId)
		{
			throw new NotImplementedException();
		}

		public Task UpdateQuantityAsync(UpdateBasketItemVM updateBasketItem)
		{
			throw new NotImplementedException();
		}
	}
}
