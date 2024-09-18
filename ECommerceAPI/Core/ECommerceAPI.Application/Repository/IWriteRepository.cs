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
		Task<bool> AddRangeAsync(List<T> datas);
		Task<bool> RemoveAsync(string id);
		bool Remove(T model);
		bool RemoveRange(List<T> datas);	
		bool Update(T model);
		Task<int> SaveAsync();
	}
}
