using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;

		public UserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserResponse> CreateAsync(CreateUser user)
		{
			IdentityResult result = await _userManager.CreateAsync(new()
			{
				Name = user.Name,
				Email = user.Email,
				UserName = user.Username
			}, user.Password);

			if (result.Succeeded)
			{
				return new()
				{
					Succeeded = result.Succeeded,
					Message = "Operation is succeeded"
				};
			}
			else
			{
				throw new UserCreateException();
			}
		}

		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenLifeTime)
		{

			if (user == null)
			{
				throw new UserNotFoundException("user not found");
			}
			else
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenEndDate = accessTokenDate.AddMinutes(refreshTokenLifeTime);
				await _userManager.UpdateAsync(user);
			}

		}
	}
}
