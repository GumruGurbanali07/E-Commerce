using ECommerceAPI.Application.Repository;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repository
{
	public class ProductWriteRepository : WriteRepository<CeateProductVM>, IProductWriteRepository
	{
		public ProductWriteRepository(AppDbContext context) : base(context)
		{
		}
	}
}
