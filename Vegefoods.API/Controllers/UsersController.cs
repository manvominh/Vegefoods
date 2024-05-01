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
using Vegefoods.Application.Features.UserFeatures.Command.ChangePassword;
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
		public async Task<ActionResult> Register([FromBody] UserDtoRegistration registerUser, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(new RegisterUserQuery(registerUser), cancellationToken);
			
			return Ok(response);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
		public async Task<ActionResult> Login([FromBody] UserDtoLogIn loginUser, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(new GetUserByEmailAndPasswordQuery(loginUser), cancellationToken);
			//if (!response.IsSuccess)
			//	throw new BadRequestException($"User Credential is invalid.");

			return Ok(response);
		}
		//[Authorize]
		[HttpGet("GetUserByEmail/{email}")]
		public async Task<ActionResult> GetUserByEmail(string email, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(new GetUserByEmailQuery(email), cancellationToken);
			return Ok(response);
		}
		//[Authorize]
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateProfile(int id, UserDtoProfile userProfile, CancellationToken cancellationToken)
		{
			if (userProfile.Id != id)
				throw new BadRequestException($"Invalid Id: {id}");

			var response = await _mediator.Send(new UpdateUserQuery(userProfile), cancellationToken);
			if(response.IsSuccess)
				return Ok(response);

			throw new BadRequestException("Updated user failed.");
		}
		//[Authorize]
		[HttpPost("ChangePassword")]
		public async Task<ActionResult> ChangePassword([FromBody] PasswordDto passwordDto, CancellationToken cancellationToken)
		{
			var response = await _mediator.Send(new ChangePasswordQuery(passwordDto), cancellationToken);
			return Ok(response);
		}
	}
}
