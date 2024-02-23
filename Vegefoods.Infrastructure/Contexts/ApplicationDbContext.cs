
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
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

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			// Product
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 1,
				Name = "Bell Pepper",
				Description = "Bell Pepper",
				Price = 110,
				ImageUrl = "product1",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 2,
				Name = "Strawberry",
				Description = "Strawberry",
				Price = 130,
				ImageUrl = "product2",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 3,
				Name = "Green Beans",
				Description = "Green Beans",
				Price = 100,
				ImageUrl = "product3",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 4,
				Name = "Purple Cabbage",
				Description = "Purple Cabbage",
				Price = 95,
				ImageUrl = "product4",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 5,
				Name = "Tomatoe",
				Description = "Tomatoe",
				Price = 120,
				ImageUrl = "product5",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 6,
				Name = "Brocolli",
				Description = "Brocolli",
				Price = 120,
				ImageUrl = "product6",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 7,
				Name = "Carrots",
				Description = "Carrots",
				Price = 98,
				ImageUrl = "product7",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 8,
				Name = "Fruit Juice",
				Description = "Fruit Juice",
				Price = 110,
				ImageUrl = "product2",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 9,
				Name = "Onion",
				Description = "Onion",
				Price = 100,
				ImageUrl = "product3",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 10,
				Name = "Apple",
				Description = "Apple",
				Price = 65,
				ImageUrl = "product1",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 11,
				Name = "Chilli",
				Description = "Chilli",
				Price = 60,
				ImageUrl = "product2",
			});
			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 12,
				Name = "Garlic",
				Description = "Garlic",
				Price = 75,
				ImageUrl = "product3",
			});
		}
	}
}
