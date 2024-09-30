using ECommerceAPI.Application.Repository;
using ECommerceAPI.Application.ViewModel.Product;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;

		public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
		}
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok (_productReadRepository.GetAll());
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


	}
}
