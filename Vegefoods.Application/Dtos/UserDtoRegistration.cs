
using System.ComponentModel.DataAnnotations;
using Vegefoods.Application.Common.Mappings;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Dtos
{
	public class UserDtoRegistration : IMapFrom<User>
	{

		[Required(ErrorMessage = "Email can not be empty")]
		[EmailAddress(ErrorMessage = "Please enter a valid email")]
		[MaxLength(50)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password can not be empty")]
		[MinLength(8, ErrorMessage = "Password should be between 8 and 50 characters.")]
		[MaxLength(50, ErrorMessage = "Password should be between 8 and 50 characters.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password can not be empty")]
		[MinLength(8, ErrorMessage = "Password should be between 8 and 50 characters.")]
		[MaxLength(50, ErrorMessage = "Password should be between 8 and 50 characters.")]
		[Compare("Password", ErrorMessage ="Confirm Password does not match.")]
		public string ConfirmPassword { get; set; }
	}
}
