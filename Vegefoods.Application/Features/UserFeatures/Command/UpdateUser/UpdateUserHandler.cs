using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.UpdateUser
{
	public record UpdateUserQuery(UserProfileDto UserProfileDto) : IRequest<(bool isUpdated, UserProfileDto userDto)>;
	public class UpdateUserHandler : IRequestHandler<UpdateUserQuery, (bool isUpdated, UserProfileDto userDto)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<(bool isUpdated, UserProfileDto userDto)> Handle(UpdateUserQuery query, CancellationToken cancellationToken)
		{
			//var user = _mapper.Map<User>(query.UserProfileDto);
			var existedUser = await _unitOfWork.Repository<User>().Entities.Where(x => x.Id == query.UserProfileDto.Id).FirstAsync(cancellationToken);
			var user = new User()
			{
				Id = query.UserProfileDto.Id,
				Email = existedUser.Email,
				Password = existedUser.Password,
				FirstName = query.UserProfileDto.FirstName,
				LastName = query.UserProfileDto.LastName,
				DateOfBirth = query.UserProfileDto.DateOfBirth,
				Address = query.UserProfileDto.Address,
				Country = query.UserProfileDto.Country,
				Phone = query.UserProfileDto.Phone,
				Gender = query.UserProfileDto.Gender,
			};
			await _unitOfWork.Repository<User>().UpdateAsync(user);
			await _unitOfWork.Save(cancellationToken);

			return (true, _mapper.Map<UserProfileDto>(user));
		}
	}
}
