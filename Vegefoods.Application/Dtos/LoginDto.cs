using System.ComponentModel.DataAnnotations;

namespace Vegefoods.Application.Dtos
{
	public class LoginRequest
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
