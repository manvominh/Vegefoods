
namespace Vegefoods.Application.Dtos
{
	public class PasswordDto
	{
		public int Id { get; set; }
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
