﻿using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repository
{
	public interface IReadRepository<T> :IRepository<T>	where T : BaseEntity
	{
		IQueryable<T> GetAll();
		Task<T> GetByIdAsync(string id);
		IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
		Task<T> GetSingleAsync(Expression<Func<T, bool>> method);

	}
}
