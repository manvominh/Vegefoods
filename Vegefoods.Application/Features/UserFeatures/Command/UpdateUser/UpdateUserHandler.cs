using AutoMapper;
using MediatR;
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.UpdateUser
{
	public record UpdateUserQuery(int Id, ProfileUserDto ProfileUser) : IRequest<ProfileUserDto>;
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
			try
			{
				//var user = _mapper.Map<User>(query);
				var user = new User()
				{

					Id = query.Id,
					Email = query.ProfileUser.Email,
					Password = HashHelper.HashPassword(query.ProfileUser.Password),
					FirstName = query.ProfileUser.FirstName,
					LastName = query.ProfileUser.LastName,
					DateOfBirth = query.ProfileUser.DateOfBirth,
					Address = query.ProfileUser.Address,
					Country = query.ProfileUser.Country,
					Phone = query.ProfileUser.Phone,
					Gender = query.ProfileUser.Gender,
				};
				await _unitOfWork.Repository<User>().UpdateAsync(user);
				await _unitOfWork.Save(cancellationToken);

				return _mapper.Map<ProfileUserDto>(user);
			}
			catch(Exception ex)
			{
				_unitOfWork.Rollback();
			}
			return new ProfileUserDto();
		}
	}
}
