using System.ComponentModel.DataAnnotations;
using Vegefoods.Domain.Common;

namespace Vegefoods.Domain.Entities
{
	public class Product : BaseAuditableEntity
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		[Required]
		[StringLength(500)]
		public string Description { get; set; }
		[Required]
		public float Price { get;set; }
		[Required]
		public string ImageUrl { get; set; }
	}
}
