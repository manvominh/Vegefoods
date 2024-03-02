using AutoMapper;
using MediatR;
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.UpdateUser
{
	public record UpdateUserQuery(UserDto UserDto) : IRequest<(bool isUpdated, UserDto userDto)>;
	public class UpdateUserHandler : IRequestHandler<UpdateUserQuery, (bool isUpdated, UserDto userDto)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<(bool isUpdated, UserDto userDto)> Handle(UpdateUserQuery query, CancellationToken cancellationToken)
		{
			//var user = _mapper.Map<User>(query);
			var user = new User()
			{

				Id = query.UserDto.Id,
				Email = query.UserDto.Email,
				Password = HashHelper.HashPassword(query.UserDto.Password),
				FirstName = query.UserDto.FirstName,
				LastName = query.UserDto.LastName,
				DateOfBirth = query.UserDto.DateOfBirth,
				Address = query.UserDto.Address,
				Country = query.UserDto.Country,
				Phone = query.UserDto.Phone,
				Gender = query.UserDto.Gender,
			};
			await _unitOfWork.Repository<User>().UpdateAsync(user);
			await _unitOfWork.Save(cancellationToken);

			return (true, _mapper.Map<UserDto>(user));
		}
	}
}
