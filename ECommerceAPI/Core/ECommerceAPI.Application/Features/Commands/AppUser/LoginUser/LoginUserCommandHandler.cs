﻿using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommerceAPI.Application.Features.Commands.AppUser.LoginUser.LoginUserCommandResponse;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
		private readonly UserManager<ECommerceAPI.Domain.Entities.Identity.AppUser> _userManager;
		private readonly SignInManager<ECommerceAPI.Domain.Entities.Identity.AppUser> _signInManager;
		private readonly ITokenHandler _tokenHandler;
		public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
			ECommerceAPI.Domain.Entities.Identity.AppUser user=await _userManager.FindByNameAsync(request.UsernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
			}
			if (user == null) {
				throw new UserNotFoundException("User not found");
			}

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			if (result.Succeeded) {
				Token token = _tokenHandler.CreateAccessToken(5);
				return  new LoginUserSuccessCommandResponse()
				{
					token = token,
				};
			}
			return new LoginUserErrorCommandResponse()
			{
				Message = "User can not login"
			};
		}
	}
}
