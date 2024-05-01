
using AutoMapper;
using MediatR;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.CreateUser
{
	public record CreateUserQuery(UserDto User) : IRequest<(bool isCreated, UserDto userDto)>;
	public class CreateUserHandler : IRequestHandler<CreateUserQuery, (bool isCreated, UserDto userDto)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<(bool isCreated, UserDto userDto)> Handle(CreateUserQuery query, CancellationToken cancellationToken)
		{
			var user = _mapper.Map<User>(query);
			await _unitOfWork.Repository<User>().AddAsync(user);
			await _unitOfWork.Save(cancellationToken);

			return (true, _mapper.Map<UserDto>(user));
		}
	}
}
