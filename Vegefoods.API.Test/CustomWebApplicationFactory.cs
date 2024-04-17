
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Reflection;
using Vegefoods.Persistence.Contexts;

namespace Vegefoods.API.Test
{
	public class CustomWebApplicationFactory<IProgram> : WebApplicationFactory<IProgram> where IProgram : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			base.ConfigureWebHost(builder);
			builder.ConfigureTestServices(services =>
			{
				var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
				services.Remove(dbContextDescriptor);
				var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
				services.Remove(dbConnectionDescriptor);

				services.AddSingleton<DbConnection>(container =>
				{
					var connection = new SqliteConnection("DataSource=:memory:");
					connection.Open();
					return connection;
				});

				services.AddDbContext<ApplicationDbContext>((container, option) =>
				{
					var connection = container.GetRequiredService<DbConnection>();
					option.UseSqlite(connection);
				});
			});
			builder.UseEnvironment("Development");
		}
	}
	
}
