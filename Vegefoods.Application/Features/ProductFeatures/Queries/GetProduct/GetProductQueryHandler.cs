using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.ProductFeatures
{
	public record GetProductQuery(int Id) : IRequest<ProductDetailsDto>;
	internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDetailsDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ProductDetailsDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			var product = await _unitOfWork.Repository<Product>().Entities
						.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
						.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			if (product == null)
				return new ProductDetailsDto() { IsSuccess = false, Message = "Product is not existed.", ProductDto = new ProductDto() };

			return new ProductDetailsDto() { IsSuccess = true, Message = "Get Product successfully.", ProductDto = product };
		}
	}
}
