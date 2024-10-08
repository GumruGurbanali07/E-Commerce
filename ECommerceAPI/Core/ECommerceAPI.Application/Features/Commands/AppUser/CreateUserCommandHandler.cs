using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ECommerceAPI.Domain.Entities.Identity;
namespace ECommerceAPI.Application.Features.Commands.AppUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		private readonly UserManager<P.AppUser> _userManager;

		public CreateUserCommandHandler(UserManager<P.AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			IdentityResult result = await _userManager.CreateAsync(new()
			{
				Name = request.Name,
				Email = request.Email,
				UserName = request.Username
			}, request.Password);
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
	}
}
