﻿using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ECommerceAPI.Domain.Entities.Identity;
namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		private readonly IUserService _userService;

		public CreateUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			CreateUserResponse response = await _userService.CreateAsync(new()
			{
				Email = request.Email,
				Name = request.Name,
				Password = request.Password,
				PasswordConfirmation = request.PasswordConfirmation,
				Username = request.Username,
			});
			return new()
			{
				Message = response.Message,
				Succeeded = response.Succeeded,
			};
		}
	}
}
