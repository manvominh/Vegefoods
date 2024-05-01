using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.UserFeatures.Command.RegisterUser
{
	public record RegisterUserQuery(UserDtoRegistration RegisterUser) : IRequest<ReturnModel>;
	public class RegisterUserHandler : IRequestHandler<RegisterUserQuery, ReturnModel>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ReturnModel> Handle(RegisterUserQuery query, CancellationToken cancellationToken)
		{
			var isExistedUser = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(x => x.Email == query.RegisterUser.Email, cancellationToken);
			if(isExistedUser != null)
				return new ReturnModel() { IsSuccess = false, Message = "User is existed." };

			var user = new User()
			{
				Email = query.RegisterUser.Email,
				Password = HashHelper.HashPassword(query.RegisterUser.Password),
			};
			await _unitOfWork.Repository<User>().AddAsync(user);
			await _unitOfWork.Save(cancellationToken);

			return new ReturnModel() { IsSuccess = true, Message = "User registed successfully." };
		}
	}
}
