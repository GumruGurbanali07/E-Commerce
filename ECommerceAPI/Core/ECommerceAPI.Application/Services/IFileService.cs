using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Services
{
	public interface IFileService
	{
		Task<List<(string fileName, string path)>> FileUploadAsync(string path, IFormFileCollection files);
		Task<string> RenameFileAsync(string path);
		Task<bool> CopyFileAsync(string path, IFormFile formFile);
	}
}
