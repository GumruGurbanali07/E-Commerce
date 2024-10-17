using ECommerceAPI.Application.Abstraction.Token;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Token
{
	public class TokenHandler : ITokenHandler
	{
		private readonly IConfiguration _configuration;

		public TokenHandler(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Application.DTOs.Token CreateAccessToken(int minute)
		{
			Application.DTOs.Token token = new();
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
			token.Expiration = DateTime.UtcNow.AddMinutes(20);
			JwtSecurityToken securityToken = new(
				audience: _configuration["Token:Audience"],
				issuer: _configuration["Token:Issures"],
				expires:token.Expiration,
				notBefore:DateTime.UtcNow,
				signingCredentials:signingCredentials);

			JwtSecurityTokenHandler tokenHandler = new();
			token.AccessToken=tokenHandler.WriteToken(securityToken);
			token.RefreshToken=CreateRefreshToken(minute);
			return token;	
		}

		public string CreateRefreshToken(int minute)
		{
			byte[] number = new byte[32];
			using RandomNumberGenerator random=RandomNumberGenerator.Create();
			random.GetBytes(number);
			return Convert.ToBase64String(number);
		}
	}
}
