using AutoMapper;
using MediatR;
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.RegisterUser
{
	public record RegisterUserQuery(UserRequestDto RegisterUser) : IRequest<UserRequestDto>;
	public class RegisterUserHandler : IRequestHandler<RegisterUserQuery, UserRequestDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<UserRequestDto> Handle(RegisterUserQuery query, CancellationToken cancellationToken)
		{
			//var user = _mapper.Map<User>(query.RegisterUser);
			var user = new User()
			{
				Email = query.RegisterUser.Email,
				Password = HashHelper.HashPassword(query.RegisterUser.Password),
			};
			await _unitOfWork.Repository<User>().AddAsync(user);
			await _unitOfWork.Save(cancellationToken);

			return _mapper.Map<UserRequestDto>(user);
		}
	}
}
