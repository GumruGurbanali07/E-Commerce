using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.DTOs.Order;
using ECommerceAPI.Application.Repository;
using ECommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderWriteRepository _orderWriteRepository;
		private readonly IOrderReadRepository _orderReadRepository;

		public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
		{
			_orderWriteRepository = orderWriteRepository;
			_orderReadRepository = orderReadRepository;
		}

		public async Task CreateOrderAsync(CreateOrder createOrder)
		{
			var orderCode = (new Random().NextDouble() * 10000).ToString();
			orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);
			await _orderWriteRepository.AddAsync(new()
			{
				Address = createOrder.Address,
				Id = Guid.Parse(createOrder.BasketId),
				Description = createOrder.Description,
				OrderCode = orderCode,
			});
			await _orderWriteRepository.SaveAsync();
		}

		public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
		{
			var query = _orderReadRepository.GetAll().Include(x => x.Basket)
				.ThenInclude(y => y.User)
				.Include(x => x.Basket)
				.ThenInclude(i => i.BasketItems)
				.ThenInclude(p => p.Product);

			var data = query.Skip(page * size).Take(size);

			return new()
			{
				TotalOrderCount = await query.CountAsync(),
				Orders = await data.Select(l => new
				{
					Id = l.Id,
					CreatedDate = l.CreatedDate,
					OrderCode = l.OrderCode,
					TotalPrice = l.Basket.BasketItems.Sum(s => s.Product.Price * s.Quantity),
					Username = l.Basket.User.UserName
				}).ToListAsync()
			};

			//.Select(l => new ListOrder
			//{
			//	CreatedDate = l.CreatedDate,
			//	OrderCode = l.OrderCode,
			//	TotalPrice = l.Basket.BasketItems.Sum(s => s.Product.Price * s.Quantity),
			//	Username = l.Basket.User.UserName
			//}).Skip(page*size).Take(size).ToListAsync();


		}

		public async Task<SingleOrder> GetOrderByIdAsync(string id)
		{
			var data = await _orderReadRepository.GetAll()
					.Include(x => x.Basket)
					.ThenInclude(y => y.BasketItems)
					.ThenInclude(z => z.Product)
					.FirstOrDefaultAsync(j => j.Id == Guid.Parse(id));

			return new()
			{
				Id = data.Id.ToString(),
				BasketItem = data.Basket.BasketItems.Select(x => new
				{
					x.Product.Name,
					x.Product.Price,
					x.Quantity
				}),
				Address = data.Address,
				CreatedDate = data.CreatedDate,
				OrderCode = data.OrderCode,
				Description = data.Description,

			};
		}
	}
}
