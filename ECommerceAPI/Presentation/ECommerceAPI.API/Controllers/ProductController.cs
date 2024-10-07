using ECommerceAPI.Application.Repository;
using ECommerceAPI.Application.ViewModel.Product;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.Services;
using ECommerceAPI.Application.Features.Queries;
using MediatR;
using ECommerceAPI.Application.Features.Commands;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMediator _mediator;

		public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IMediator mediator)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
			_webHostEnvironment = webHostEnvironment;
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllProductRequest request)
		{
			GetAllProductResponse response=await _mediator.Send(request);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateProductCommandRequest request)
		{
			CreateProductCommandResponse response =await _mediator.Send(request);			
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
