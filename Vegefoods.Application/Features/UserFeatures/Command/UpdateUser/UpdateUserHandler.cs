using AutoMapper;
using MediatR;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.UpdateUser
{
	public record UpdateUserQuery(ProfileUserDto ProfileUser) : IRequest<ProfileUserDto>;
	public class UpdateUserHandler : IRequestHandler<UpdateUserQuery, ProfileUserDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ProfileUserDto> Handle(UpdateUserQuery query, CancellationToken cancellationToken)
		{
			var user = _mapper.Map<User>(query);
			await _unitOfWork.Repository<User>().UpdateAsync(user);
			await _unitOfWork.Save(cancellationToken);

			return _mapper.Map<ProfileUserDto>(user);
		}
	}
}
