using ECommerceAPI.Application.Repository;
using ECommerceAPI.Application.ViewModel.Product;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.Services;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IFileService _fileService;
		public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment,IFileService fileService)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
			_webHostEnvironment = webHostEnvironment;
			_fileService = fileService;
		}
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] Pagination pagination)
		{
			var totalCount = _productReadRepository.GetAll(false).Count();
			var products = _productReadRepository.GetAll(false).Select(x => new
			{
				x.Id,
				x.Name,
				x.Price,
				x.Stock,
				x.CreatedDate,
				x.UpdateDate,
			}).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();

			return Ok(new
			{
				products,
				totalCount
			});
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateProductVM productVM)
		{

			await _productWriteRepository.AddAsync(new()
			{
				Name = productVM.Name,
				Price = productVM.Price,
				Stock = productVM.Stock,
			});
			await _productWriteRepository.SaveAsync();
			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut]
		public async Task<IActionResult> Put(UpdateProductVM updateProductVM)
		{
			Product product = await _productReadRepository.GetByIdAsync(updateProductVM.Id);
			product.Price = updateProductVM.Price;
			product.Stock = updateProductVM.Stock;
			product.Name = updateProductVM.Name;
			await _productWriteRepository.SaveAsync();
			return Ok();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _productWriteRepository.RemoveAsync(id);
			await _productWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Upload()
		{
			await _fileService.FileUploadAsync("resource/product-images",Request.Form.Files);
			return Ok();
		}

	}
}
