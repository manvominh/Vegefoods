
using System.ComponentModel.DataAnnotations;
using Vegefoods.Application.Common.Mappings;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Dtos
{
	public class UserDto : IMapFrom<User>
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Email can not be empty.")]
		[MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password can not be empty.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "First Name can not be empty.")]
		[MaxLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name can not be empty.")]
		[MaxLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Gender can not be empty.")]
		[MaxLength(10, ErrorMessage = "Gender cannot be longer than 10 characters.")]
		public string Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		[MaxLength(800, ErrorMessage = "Address cannot be longer than 800 characters.")]
		public string? Address { get; set; }
		[MaxLength(10, ErrorMessage = "Gender cannot be longer than 10 characters.")]
		public string Country { get; set; }
		[MaxLength(10, ErrorMessage = "Gender cannot be longer than 10 characters.")]
		public int Phone { get; set; } 		
	}
}
