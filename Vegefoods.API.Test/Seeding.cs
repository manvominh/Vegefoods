
using Vegefoods.Application.Common.Helpers;
using Vegefoods.Domain.Entities;
using Vegefoods.Persistence.Contexts;

namespace Vegefoods.API.Test
{
	public class Seeding
	{					   		
		public static void InitializeDB(ApplicationDbContext db)
		{
			db.Users.AddRange(GetUsers());
			db.SaveChanges();
		}
		private static List<User> GetUsers()
		{ 
			return new List<User>()
			{
				new User() { Id = 1, Email="tester@test.com", Password = HashHelper.HashPassword("123")},
				new User() { Id = 2, Email="test01@test.com", Password = HashHelper.HashPassword("123")},
				new User() { Id = 3, Email="testlogin@test.com", Password = HashHelper.HashPassword("123")},
			}; 
		}
		
	}
}
