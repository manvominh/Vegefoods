using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Queries.GetUserByEmail
{
	public record GetUserByEmailQuery(string Email) : IRequest<UserDtoResponse>;
	public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDtoResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetUserByEmailHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<UserDtoResponse> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
		{
			var user = await _unitOfWork.Repository<User>().Entities
				.FirstOrDefaultAsync(x => x.Email == query.Email, cancellationToken);
			if (user == null)
				return new UserDtoResponse() { IsSuccess = false, Message = $"Get User By Email is not existed: {query.Email}", UserDto = new UserDto() };

			return new UserDtoResponse() { IsSuccess = true, Message = $"Get User By Email is successfully.", UserDto = _mapper.Map<UserDto>(user) };
		}
	}
}
