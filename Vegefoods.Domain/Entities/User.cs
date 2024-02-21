
using Vegefoods.Domain.Common;

namespace Vegefoods.Domain.Entities
{
	public class User : BaseEntity
	{
		public int Id { get;set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LasstName { get; set; }
		public string Address { get; set; }
		public string Country { get; set; }	
		public string Phone { get; set; }
		public string Gender { get; set; }
	}
}
