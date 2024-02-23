
using System.ComponentModel.DataAnnotations;
using Vegefoods.Application.Common.Mappings;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Dtos
{
	public class UserRequestDto : IMapFrom<User>
	{

		[Required(ErrorMessage = "Email can not be empty")]
		[EmailAddress(ErrorMessage = "Please enter a valid email")]
		[MaxLength(50)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password can not be empty")]
		[MaxLength(50)]
		public string Password { get; set; }
	}
}
