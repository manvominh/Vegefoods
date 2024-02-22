using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Threading;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] UserDto userDto, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(userDto, cancellationToken);
			return Ok(response);
		}
	}
}
