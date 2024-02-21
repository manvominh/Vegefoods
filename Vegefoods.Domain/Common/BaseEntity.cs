using Vegefoods.Domain.Common.Interfaces;

namespace Vegefoods.Domain.Common
{
	public class BaseEntity : IEntity
	{
		public int Id { get; set; }
	}
}
