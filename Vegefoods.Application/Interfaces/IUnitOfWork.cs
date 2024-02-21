

using Vegefoods.Domain.Common.Interfaces;

namespace Vegefoods.Application.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<T> Repository<T>() where T : class, IEntity;

		Task<int> Save(CancellationToken cancellationToken);

		Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

		Task Rollback();
	}
}
