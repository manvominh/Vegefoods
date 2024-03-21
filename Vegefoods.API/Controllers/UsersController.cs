using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Threading;
using Vegefoods.Application.Common.Exceptions;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Features.ProductFeatures;
using Vegefoods.Application.Features.UserFeatures.Command.RegisterUser;
using Vegefoods.Application.Features.UserFeatures.Command.UpdateUser;
using Vegefoods.Application.Features.UserFeatures.Queries.GetUserByEmail;
using Vegefoods.Application.Features.UserFeatures.Queries.GetUserByEmailAndPassword;
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
		public async Task<ActionResult> Register([FromBody] UserRequestDto registerUser, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(new RegisterUserQuery(registerUser), cancellationToken);
			return Ok(response);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
		public async Task<ActionResult> Login([FromBody] UserRequestDto loginUser, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(new GetUserByEmailAndPasswordQuery(loginUser), cancellationToken);
			if (!response.IsSuccess)
				throw new BadRequestException("User not found.");
			return Ok(response);
		}
		[Authorize]
		[HttpGet("GetUserByEmail/{email}")]
		public async Task<ActionResult> GetUserByEmail(string email, CancellationToken cancellationToken)
		{
			return Ok(await _mediator.Send(new GetUserByEmailQuery(email), cancellationToken));
		}
		[Authorize]
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateProfile(int id, UserDto user, CancellationToken cancellationToken)
		{
			if (user.Id != id)
				throw new BadRequestException($"Invalid Id: {id}");

			var response = await _mediator.Send(new UpdateUserQuery(user), cancellationToken);
			if(response.isUpdated)
				return Ok(response);

			throw new BadRequestException("Updated user failed.");
		}
	}
}
