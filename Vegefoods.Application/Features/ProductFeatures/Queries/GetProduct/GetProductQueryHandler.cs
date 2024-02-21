using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.ProductFeatures
{
	public record GetProductQuery(int Id) : IRequest<ProductDto>;
	internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<Product>().Entities
						.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
						.Where(x => x.Id == request.Id)
						.FirstAsync(cancellationToken);
		}
	}
}
