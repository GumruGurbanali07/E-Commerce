﻿using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services
{
	public interface IUserService
	{
		Task<CreateUserResponse> CreateAsync(CreateUser user);
		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenLifeTime);
	}
}
