using ECommerceAPI.Application.Repository;
using ECommerceAPI.Application.ViewModel.Product;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.Services;
using MediatR;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetProductById;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		
		private readonly IMediator _mediator;

		public ProductController( IMediator mediator)
		{
			
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] GetAllProductRequest request)
		{
			GetAllProductResponse response = await _mediator.Send(request);
			return Ok(response);
		}
		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute] GetProductRequest request)
		{
			GetProductResponse response=await _mediator.Send(request);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateProductCommandRequest request)
		{
			CreateProductCommandResponse response =await _mediator.Send(request);			
			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest request)
		{
			UpdateProductCommandResponse response=await _mediator.Send(request);
			return Ok(response);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
		{
			RemoveProductCommandResponse response=await _mediator.Send(request);
			return Ok(response);
		}


	}
}
