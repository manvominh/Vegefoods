
using System.ComponentModel.DataAnnotations;
using Vegefoods.Application.Common.Mappings;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Dtos
{
	public class UserDto : IMapFrom<User>
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Email can not be empty.")]
		[MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password can not be empty.")]
		[MaxLength(50, ErrorMessage = "Password cannot be longer than 50 characters.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password can not be empty.")]
		[MaxLength(50, ErrorMessage = "Confirm Password cannot be longer than 50 characters.")]
		[Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
		public string ConfirmPassword { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
	}
}
