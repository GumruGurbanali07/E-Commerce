using ECommerceAPI.Application.Repository;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repository
{
	public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
	{
		private readonly AppDbContext _context;

		public WriteRepository(AppDbContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public Task<bool> AddAsync(T model)
		{
			throw new NotImplementedException();
		}

		public Task<bool> AddRangeAsync(List<T> model)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(T model)
		{
			throw new NotImplementedException();
		}

		public Task<int> SaveAsync()
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(T model)
		{
			throw new NotImplementedException();
		}
	}
}
