using ECommerceAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services
{
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public FileService(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<bool> CopyFileAsync(string path, IFormFile formFile)
		{
			try
			{
				await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
				await fileStream.CopyToAsync(fileStream);
				await fileStream.FlushAsync();
				return true;
			}
			catch (Exception ex)
			{
				{
					throw ex;
				}

			}
		}

		public async Task<List<(string fileName, string path)>> FileUploadAsync(string path, IFormFileCollection files)
		{
			string fileUpload = Path.Combine(_webHostEnvironment.WebRootPath, path);
			if (!Directory.Exists(fileUpload))
			{
				Directory.CreateDirectory(fileUpload);
			}
			List<(string fileName, string path)> datas = new();
			List<bool> results = new();
			foreach (IFormFile file in files)
			{
				string fileNewName = await RenameFileAsync(file.FileName);
				bool result=await CopyFileAsync($"{fileUpload}\\{fileNewName}", file);
				datas.Add((fileNewName, $"{fileUpload}\\{fileNewName}"));
				results.Add(result);
			}
			if (results.TrueForAll(x => x.Equals(true))) {

				return datas;
			}
			return null;

		}

		public Task<string> RenameFileAsync(string path)
		{
			throw new NotImplementedException();
		}
	}
}
