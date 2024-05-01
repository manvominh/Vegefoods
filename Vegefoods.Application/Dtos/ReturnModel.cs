
namespace Vegefoods.Application.Dtos
{
	public record ReturnModel
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}
