
using System.ComponentModel.DataAnnotations;

namespace Vegefoods.Application.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }

		
	}
}
