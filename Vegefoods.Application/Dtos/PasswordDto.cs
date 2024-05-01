
using System.ComponentModel.DataAnnotations;

namespace Vegefoods.Application.Dtos
{
	public class PasswordDto
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Password can not be empty")]
		[MinLength(8, ErrorMessage = "Password should be between 8 and 50 characters.")]
		[MaxLength(50, ErrorMessage = "Password should be between 8 and 50 characters.")]
		public string CurrentPassword { get; set; }
		[Required(ErrorMessage = "Current Password can not be empty")]
		[MinLength(8, ErrorMessage = "Current Password should be between 8 and 50 characters.")]
		[MaxLength(50, ErrorMessage = "Current Password should be between 8 and 50 characters.")]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirm Password can not be empty")]
		[MinLength(8, ErrorMessage = "Confirm Password should be between 8 and 50 characters.")]
		[MaxLength(50, ErrorMessage = "Confirm Password should be between 8 and 50 characters.")]
		[Compare("NewPassword", ErrorMessage = "Confirm Password does not match.")]
		public string ConfirmPassword { get; set; }
	}
}
