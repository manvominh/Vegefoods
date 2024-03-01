
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Vegefoods.Domain.Common;

namespace Vegefoods.Domain.Entities
{
	public class User : BaseEntity
	{
		[StringLength(50)]
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[StringLength(50)]
		public string? FirstName { get; set; }
		[StringLength(50)]
		public string? LastName { get; set; }
		public DateTime? DateOfBirth { get; set; }
		[StringLength(200)]
		public string? Address { get; set; }
		[StringLength(50)]
		public string? Country { get; set; }
		[StringLength(15)]
		public string? Phone { get; set; }
		[StringLength(10)]
		public string? Gender { get; set; }
	}
}
