
namespace Vegefoods.Application.Dtos
{
	public record ProductDetailsDto
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public ProductDto ProductDto { get; set; }
	}
}
