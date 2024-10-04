using ECommerceAPI.Application.Abstractions.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{
	public class LocalStorage : ILocalStorage
	{
		public Task DeleteAsync(string pathOrContainerName, string fileName)
		{
			throw new NotImplementedException();
		}

		public List<string> GetFiles(string pathOrContainerName)
		{
			throw new NotImplementedException();
		}

		public bool HasFile(string pathOrContainerName, string fileName)
		{
			throw new NotImplementedException();
		}

		public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName)
		{
			throw new NotImplementedException();
		}
	}
}
