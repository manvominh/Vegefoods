using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Features.UserFeatures.Command.UpdateUser;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.ChangePassword
{
	public record ChangePasswordQuery(PasswordDto PasswordDto) : IRequest<bool>;
	public class ChangePasswordHandler : IRequestHandler<ChangePasswordQuery, bool>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ChangePasswordHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<bool> Handle(ChangePasswordQuery query, CancellationToken cancellationToken)
		{
			bool result = false;
			var existedUser = await _unitOfWork.Repository<User>().Entities.Where(x => x.Id == query.PasswordDto.Id).FirstOrDefaultAsync(cancellationToken);
			if (existedUser != null)
			{
				if(HashHelper.HashPassword(query.PasswordDto.CurrentPassword) != existedUser.Password)
					return result;

				var user = new User()
				{
					Id = query.PasswordDto.Id,
					Email = existedUser.Email,
					Password = HashHelper.HashPassword(query.PasswordDto.NewPassword),
					FirstName = existedUser.FirstName,
					LastName = existedUser.LastName,
					DateOfBirth = existedUser.DateOfBirth,
					Address = existedUser.Address,
					Country = existedUser.Country,
					Phone = existedUser.Phone,
					Gender = existedUser.Gender,
				};
				await _unitOfWork.Repository<User>().UpdateAsync(user);
				await _unitOfWork.Save(cancellationToken);

				result = true;
			}
			
			return result;
			
		}
	}
}
