using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.Repository;
using ECommerceAPI.Application.ViewModel.Basket;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class BasketService : IBasketService
	{
		readonly IHttpContextAccessor _httpContextAccessor;
		readonly UserManager<AppUser> _userManager;
		readonly IOrderReadRepository _orderReadRepository;
		readonly IBasketWriteRepository _basketWriteRepository;
		readonly IBasketReadRepository _basketReadRepository;
		readonly IBasketItemWriteRepository _basketItemWriteRepository;
		readonly IBasketItemReadRepository _basketItemReadRepository;
		public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_orderReadRepository = orderReadRepository;
			_basketWriteRepository = basketWriteRepository;
			_basketItemWriteRepository = basketItemWriteRepository;
			_basketItemReadRepository = basketItemReadRepository;
			_basketReadRepository = basketReadRepository;
		}
		private async Task<Basket?> ContextUser()
		{
			//Bu sətir, cari istifadəçinin adını (username) əldə edir.
			//IHttpContextAccessor vasitəsilə HTTP sorğusu kontekstinə (session) daxil olunur və Name dəyəri əldə edilir.
			//Əgər username null və ya boş deyilsə, istifadəçi giriş edib və növbəti addımlara keçilir.
			var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
			//Bu sətirdə, UserManager vasitəsilə AppUser sinfi istifadə edilərək cari istifadəçi (username) üçün məlumat bazasından sorğu edilir
			//. Include(x => x.Basket) istifadə olunaraq,
			//Basket məlumatları user ilə birlikdə gətirilir ki, gələcəkdə istifadə edilə bilinsin.
			if (!string.IsNullOrEmpty(username))
			{
				AppUser? user = await _userManager.Users
					.Include(x => x.Basket)
					.FirstOrDefaultAsync(x => x.UserName == username);

				//Bu hissədə, istifadəçinin mövcud səbətləri (user.Basket) ilə Order (sifariş) məlumatları arasında əlaqə yaradılır (join).
				//_orderReadRepository.GetAll() sifarişlərin tam siyahısını çəkir və bu məlumatlarla basket.Id və order.Id arasında qoşulma aparılır.
				//DefaultIfEmpty() metodu isə, əgər sifariş yoxdursa, boş olaraq nəticə qaytarır.
				//Bu əməliyyat hər səbətin əlaqəli bir sifarişinin olub-olmamasını yoxlamaq üçündür.
				var _basket = from basket in user.Basket
							  join order in _orderReadRepository.GetAll()
							  on basket.Id equals order.Id into BasketOrders
							  from order in BasketOrders.DefaultIfEmpty()
							  select new
							  {
								  Basket = basket,
								  Order = order,
							  };

				//_basket nəticəsi içərisində sifariş ilə əlaqəsi olmayan (yəni, Order dəyəri null olan) bir səbət axtarılır.
				//Əgər belə bir səbət varsa, targetBasket olaraq seçilir.
				//Əgər sifarişsiz səbət tapılmırsa, yeni bir səbət yaradılır(targetBasket = new()) və istifadəçinin səbətinə əlavə edilir(user.Basket.Add(targetBasket);).
				Basket? targetBasket = null;
				if (_basket.Any(b => b.Order is null))
					targetBasket = _basket.FirstOrDefault(x => x.Order is null)?.Basket;
				else
				{
					targetBasket = new();
					user.Basket.Add(targetBasket);
				}
				//Bu son addımda, _basketWriteRepository.SaveAsync() çağırılaraq bazadakı dəyişikliklər (yeni səbətin əlavə edilməsi) saxlanılır və targetBasket metodu nəticəsi olaraq qaytarılır.
				await _basketWriteRepository.SaveAsync();
				return targetBasket;
			}
			throw new Exception("Unexpected error");

		}
		public async Task AddItemToBasketAsync(CreateBasketItemVM basketItem)
		{
			Basket? basket = await ContextUser();
			if (basket!=null){
				BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(x => x.BasketId == basket.Id && x.ProductId = Guid.Parse(basketItem.ProductId)); 
			}
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
