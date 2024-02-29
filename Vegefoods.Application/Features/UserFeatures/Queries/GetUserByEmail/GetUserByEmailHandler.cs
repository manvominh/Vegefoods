

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Features.UserFeatures.Queries.GetUserByEmailAndPassword;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Queries.GetUserByEmail
{
	public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;
	public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetUserByEmailHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<UserDto> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
		{
			var user = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(x => x.Email == query.Email);

			return _mapper.Map<UserDto>(user);
		}
	}
}
