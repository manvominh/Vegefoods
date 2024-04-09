using System.ComponentModel.DataAnnotations;
using Vegefoods.Application.Common.Mappings;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Application.Dtos
{
	public class UserProfileDto : IMapFrom<User>
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
	}
}
