
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Vegefoods.Domain.Entities;

namespace Vegefoods.Persistence.Contexts
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			try
			{
				var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
				if (databaseCreator != null)
				{
					if (!databaseCreator.CanConnect()) databaseCreator.Create();
					if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public override int SaveChanges()
		{
			return SaveChangesAsync().GetAwaiter().GetResult();
		}
	}
}
