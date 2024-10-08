using ECommerceAPI.Application.Features.Commands.AppUser;
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

		public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
		{
			CreateUserCommandResponse response=await _mediator.Send(request);
			return Ok(response);
		}
	}
}
