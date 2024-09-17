using ECommerceAPI.Application.Abstract;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concrete
{
	public class ProductService : IProductService
	{
		public List<Product> GetProducts() => new()
		{
			new()
			{
				Id=Guid.NewGuid(),Name="A", Price=10,Stock=30,
			},
			new()
			{
				Id=Guid.NewGuid(),Name="B", Price=20,Stock=40,
			},
			new()
			{
				Id=Guid.NewGuid(),Name="C", Price=30,Stock=50,
			}
		};
	}
}
