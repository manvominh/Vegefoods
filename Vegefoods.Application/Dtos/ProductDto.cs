using Vegefoods.Application.Common.Mappings;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Dtos
{
	public class ProductDto : IMapFrom<Product>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageURL { get; set; }
		public decimal Price { get; set; }
	}
}
