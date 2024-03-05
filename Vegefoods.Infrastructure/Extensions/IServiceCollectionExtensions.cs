﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Vegefoods.Application.Common.Caching;
using Vegefoods.Application.Interfaces;
using Vegefoods.Persistence.Common.Caching;
using Vegefoods.Persistence.Contexts;
using Vegefoods.Persistence.Repositories;

namespace Vegefoods.Persistence.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration, string dbHost, string dbName, string dbPassword)
		{
			services.AddMappings();
			services.AddDbContext(configuration, dbHost, dbName, dbPassword);
			services.AddCaches(configuration);
			services.AddRepositories();
		}
		private static void AddCaches(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(configuration.GetRequiredSection(nameof(CacheOptions)).Get<CacheOptions>());
			services.AddTransient<ICacheService, CacheService>();
			services.AddMemoryCache();
		}		

		private static void AddMappings(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
		}

		public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, string dbHost, string dbName, string dbPassword)
		{
			var connectionString = string.Format(configuration.GetConnectionString("DefaultConnection"), dbHost, dbName, dbPassword);

			services.AddDbContext<ApplicationDbContext>(options =>
			   options.UseSqlServer(connectionString,
				   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services
				.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
				.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
			//.AddTransient<IProductRepository, ProductRepository>()
			.AddTransient<IJwtAuthenticationManagerService, JwtAuthenticationManagerService>()			
			;
		}
	}
}
