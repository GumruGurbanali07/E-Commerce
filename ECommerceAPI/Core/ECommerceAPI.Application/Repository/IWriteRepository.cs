using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repository
{
	public interface IWriteRepository<T> :IRepository<T> where T : BaseEntity
	{
		Task<bool> AddAsync(T model);
		Task<bool> AddRangeAsync(List<T> model);
		Task<bool> RemoveAsync(string id);
		Task<bool> RemoveAsync(T model);
		Task<bool> UpdateAsync(T model);
		Task<int> SaveAsync();
	}
}
