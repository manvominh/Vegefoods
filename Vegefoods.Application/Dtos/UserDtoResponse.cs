namespace Vegefoods.Application.Dtos
{
	public record UserDtoResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public UserDto UserDto { get; set; }
	}
}
