using Vegefoods.Domain.Common;

namespace Vegefoods.Domain.Entities
{
	public class Product : BaseAuditableEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public float Price { get;set; }
		public string ImageUrl { get; set; }
	}
}
