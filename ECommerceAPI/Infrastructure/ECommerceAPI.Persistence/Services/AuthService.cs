using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
		private readonly IUserService _userService;
		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
			_userService = userService;
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
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5);
				return token;
			}
			else
			throw new UserNotFoundException("User can not found");
		}

		public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
		{
		  AppUser? user=await _userManager.Users.FirstOrDefaultAsync(x=>x.RefreshToken == refreshToken);
			if(user!=null && user?.RefreshTokenEndDate > DateTime.UtcNow)
			{
				Token token = _tokenHandler.CreateAccessToken(5);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5);
				return token;
			}
			else
				throw new UserNotFoundException("not found");

		}
	}
}
