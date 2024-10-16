using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ECommerceAPI.Domain.Entities.Identity;
namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginGoogleUser
{
	public class LoginGoogleCommandHandler : IRequestHandler<LoginGoogleCommandRequest, LoginGoogleCommandResponse>
	{
		private readonly UserManager<P.AppUser> _userManager;
		private readonly ITokenHandler _tokenHandler;
		public LoginGoogleCommandHandler(UserManager<P.AppUser> userManager, ITokenHandler tokenHandler)
		{
			_userManager = userManager;
			_tokenHandler = tokenHandler;
		}
		public async Task<LoginGoogleCommandResponse> Handle(LoginGoogleCommandRequest request, CancellationToken cancellationToken)
		{
			var setting = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string> { "176397235729-9pqkphj21tnelr4ifu17m07cvrjdos3b.apps.googleusercontent.com" }
			};
			var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, setting);
			var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
			Domain.Entities.Identity.AppUser appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
			bool result = appUser != null;
			if (appUser == null)
			{
				appUser = await _userManager.FindByEmailAsync(payload.Email);
				if (appUser == null)
				{
					appUser = new()
					{
						Id = Guid.NewGuid().ToString(),
						Email = payload.Email,
						UserName = payload.Email,
						Name = payload.Name,

					};
					var identityResult = await _userManager.CreateAsync(appUser);
					result = identityResult.Succeeded;
				}
			}
			if (result)
			{
				await _userManager.AddLoginAsync(appUser, info);
			}
			else
			{
				throw new Exception("Invalid external Authentication");
			}
			Token token = _tokenHandler.CreateAccessToken(20);
			return new()
			{
				Token = token
			};
		}
	}
}
