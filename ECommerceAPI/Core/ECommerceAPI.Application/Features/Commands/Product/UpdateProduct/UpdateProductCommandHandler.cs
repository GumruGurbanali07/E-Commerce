﻿using ECommerceAPI.Application.Repository;
using ECommerceAPI.Application.ViewModel.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;

		public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
		}

		public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			ECommerceAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
			product.Price = request.Price;
			product.Stock = request.Stock;
			product.Name = request.Name;
			await _productWriteRepository.SaveAsync();
			return new();
		}
	}
}
