using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginGoogleUser;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
		{
			CreateUserCommandResponse response=await _mediator.Send(request);
			return Ok(response);
		}
		[HttpPost("[action]")]
		public async Task<IActionResult> Login(LoginUserCommandRequest request)
		{
			LoginUserCommandResponse response= await _mediator.Send(request);
			return Ok(response);
		}
		[HttpPost("login-google")]
		public async Task<IActionResult> LoginGoogle(LoginGoogleCommandRequest request)
		{
			LoginGoogleCommandResponse response= await _mediator.Send(request);
			return Ok(response);
		}
	}
}
