using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommerceAPI.Application.Features.Commands.AppUser.LoginUser.LoginUserCommandResponse;

namespace ECommerceAPI.Persistence.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private	readonly ITokenHandler _tokenHandler;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
		}

		public async Task<Token> LoginAsync(string UsernameOrEmail, string password, int tokeLifeTime)
		{
			ECommerceAPI.Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(UsernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(UsernameOrEmail);
			}
			if (user == null)
			{
				throw new UserNotFoundException("User not found");
			}

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
			if (result.Succeeded)
			{
				Token token = _tokenHandler.CreateAccessToken(tokeLifeTime);
				return token;
			}
			throw new UserNotFoundException("User can not found");
		}
	}
}
