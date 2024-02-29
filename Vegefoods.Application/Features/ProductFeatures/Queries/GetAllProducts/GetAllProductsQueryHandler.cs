using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Vegefoods.Application.Dtos;
using Vegefoods.Application.Interfaces;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Features.ProductFeatures
{
	public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _context;
		public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor context)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_context = context;
		}

		public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<Product>().Entities
				.Select(x => new ProductDto()
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description,
					Price = x.Price,
					ImageURL = $"{_context.HttpContext.Request.Scheme}://{_context.HttpContext.Request.Host}{_context.HttpContext.Request.PathBase}/Upload/ProductImages/{x.ImageUrl}"
				})
				.ToListAsync(cancellationToken);
		}
	}
}
