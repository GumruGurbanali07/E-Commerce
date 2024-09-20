using ECommerceAPI.Application.Repository;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public async Task Get() //Task olmazsa dispose olar
		{
			await _productWriteRepository.AddAsync(new() { Name = "H", Price = 1200, Stock = 20, CreatedDate = DateTime.UtcNow });
			await _productWriteRepository.SaveAsync();
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			Product product=await _productReadRepository.GetByIdAsync(id);
			return Ok(product);
		}
	}
}
