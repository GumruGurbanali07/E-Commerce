using ECommerceAPI.Application.Repository;
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
		public async void Get()
		{
			await _productWriteRepository.AddRangeAsync(new()
			{
				new() {Id=Guid.NewGuid(), Name="Product 1", Price=100,CreatedDate=DateTime.UtcNow,Stock=10},
				new() {Id=Guid.NewGuid(), Name="Product 2", Price=200,CreatedDate=DateTime.UtcNow,Stock=20},
				new() {Id=Guid.NewGuid(), Name="Product 3", Price=300,CreatedDate=DateTime.UtcNow,Stock=30},
			});
			await _productWriteRepository.SaveAsync();
		}
	}
}
