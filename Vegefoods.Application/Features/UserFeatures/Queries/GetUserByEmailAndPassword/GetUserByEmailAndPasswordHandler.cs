
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Queries.GetUserByEmailAndPassword
{
	public record GetUserByEmailAndPasswordQuery(UserRequestDto LoginUser) : IRequest<string>;
	public class GetUserByEmailAndPasswordHandler : IRequestHandler<GetUserByEmailAndPasswordQuery, string>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IJwtAuthenticationManagerService _jwtAuthenticationManagerService;

		public GetUserByEmailAndPasswordHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtAuthenticationManagerService jwtAuthenticationManagerService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_jwtAuthenticationManagerService = jwtAuthenticationManagerService;
		}

		public async Task<string> Handle(GetUserByEmailAndPasswordQuery query, CancellationToken cancellationToken)
		{
			var user = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(x => x.Email == query.LoginUser.Email && x.Password == HashHelper.HashPassword(query.LoginUser.Password));

			return user == null ? string.Empty : await _jwtAuthenticationManagerService.GenerateJwtToken(user.Email);
		}
	}
}
