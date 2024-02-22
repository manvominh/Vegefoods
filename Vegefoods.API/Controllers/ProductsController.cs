using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vegefoods.Application.Features.ProductFeatures;

namespace Vegefoods.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllProducts()
		{
			return Ok(await _mediator.Send(new GetAllProductsQuery()));
		}
		[HttpGet("{id:int}")]
		public async Task<ActionResult> GetProduct(int id)
		{
			return Ok(await _mediator.Send(new GetProductQuery(id)));
		}
		
	}
}
