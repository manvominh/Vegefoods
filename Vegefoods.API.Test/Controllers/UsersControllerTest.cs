
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Vegefoods.Application.Dtos;
using Vegefoods.Persistence.Contexts;
using Xunit;
namespace Vegefoods.API.Test.Controllers
{
	/// <summary>
	/// Add Priority to order running test - also make sure IntializeDB just once
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class TestPriorityAttribute : Attribute
	{
		public TestPriorityAttribute(int priority)
		{
			Priority = priority;
		}

		public int Priority { get; private set; }
	}
	public class UsersControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly CustomWebApplicationFactory<Program> _factory;
		private readonly HttpClient _client;
		public UsersControllerTest(CustomWebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_client = _factory.CreateClient();
		}
		[Fact, TestPriority(1)]
		public async Task Register_ReturnsOk()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var scopeServices = scope.ServiceProvider;
				var db = scopeServices.GetRequiredService<ApplicationDbContext>();

				db.Database.EnsureCreated();
				db.Database.Migrate();
				Seeding.InitializeDB(db);
			}
			// Arrange
			var newUser = new UserDtoRegistration { Email= "test@test.com", Password = "testpassword", ConfirmPassword = "testpassword" };

			// Act
			var response = await _client.PostAsync("/api/users/register", new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));

			// Assert
			response.Should().NotBeNull();
			response.EnsureSuccessStatusCode();
			var ret = await response.Content.ReadFromJsonAsync<ReturnModel>();
			ret?.IsSuccess.Should().BeTrue();
			ret?.Message.Should().Contain("successfully");
		}
		[Fact, TestPriority(2)]
		public async Task Login_ReturnsOk()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var scopeServices = scope.ServiceProvider;
				var db = scopeServices.GetRequiredService<ApplicationDbContext>();

				db.Database.EnsureCreated();
				db.Database.Migrate();
			}
			// Arrange
			var loginUser = new UserDtoLogIn { Email = "testlogin@test.com", Password = "password123" };

			// Act
			var response = await _client.PostAsync("/api/users/login", new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json"));

			// Assert
			response.EnsureSuccessStatusCode();
			var tokenObject = await response.Content.ReadFromJsonAsync<Tokens>();
			tokenObject.Should().NotBeNull();
			tokenObject?.IsSuccess.Should().BeTrue();
			tokenObject?.Token.Should().NotBeNullOrEmpty();
		}
		[Fact, TestPriority(3)]
		public async Task Login_Wrong_Credential_Returns_False()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var scopeServices = scope.ServiceProvider;
				var db = scopeServices.GetRequiredService<ApplicationDbContext>();

				db.Database.EnsureCreated();
				db.Database.Migrate();
			}
			// Arrange
			var loginUser = new UserDtoLogIn { Email = "testlogin01@test.com", Password = "password123" };

			// Act
			var response = await _client.PostAsync("/api/users/login", new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json"));

			// Assert
			//response.EnsureSuccessStatusCode();
			response.StatusCode.Equals(HttpStatusCode.OK);
			var tokenObject = await response.Content.ReadFromJsonAsync<Tokens>();
			tokenObject?.Token.Should().BeNull();
			tokenObject?.IsSuccess.Should().BeFalse();
			tokenObject.Should().NotBeNull();
		}
		private async Task<string?> GetToken()
		{
			var loginUser = new UserDtoLogIn { Email = "tester@test.com", Password = "password123" };

			var response = await _client.PostAsync("/api/users/login", new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json"));

			// Assert
			response.EnsureSuccessStatusCode();
			var tokenObject = await response.Content.ReadFromJsonAsync<Tokens>();
			return tokenObject?.Token;
		}
		[Fact, TestPriority(4)]
		public async Task GetUserByEmail_ReturnsOk()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var scopeServices = scope.ServiceProvider;
				var db = scopeServices.GetRequiredService<ApplicationDbContext>();

				db.Database.EnsureCreated();
				db.Database.Migrate();
			}
			// Arrange	 		
			var email = "test01@test.com";
			var token = await GetToken();
			HttpRequestMessage request = new(HttpMethod.Get, $"/api/users/GetUserByEmail/{email}");
			request.Headers.Add("Authorization", "Bearer " + token);

			// Act
			var response = await _client.SendAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
			var userDtoRequest = await response.Content.ReadFromJsonAsync<UserDtoResponse>();
			response.Should().NotBeNull();
			userDtoRequest?.IsSuccess.Should().BeTrue();
			userDtoRequest?.Message.Should().Contain("successfully");
		}
		[Fact, TestPriority(5)]
		public async Task GetUserByEmail_Returns_UnAuthorized()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var scopeServices = scope.ServiceProvider;
				var db = scopeServices.GetRequiredService<ApplicationDbContext>();

				db.Database.EnsureCreated();
				db.Database.Migrate();
			}
			// Arrange	 		
			var email = "test01@test.com";
			var token = await GetToken();
			HttpRequestMessage request = new(HttpMethod.Get, $"/api/users/GetUserByEmail/{email}");
			//request.Headers.Add("Authorization", "Bearer " + token);

			// Act
			var response = await _client.SendAsync(request);

			// Assert
			response.Should().NotBeNull();
			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}
		[Fact, TestPriority(6)]
		public async Task ChangePassword_ReturnsOk()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var scopeServices = scope.ServiceProvider;
				var db = scopeServices.GetRequiredService<ApplicationDbContext>();

				db.Database.EnsureCreated();
				db.Database.Migrate();
				Seeding.InitializeDB(db);
			}
			// Arrange
			var passwordDto = new PasswordDto { Id = 4, CurrentPassword = "password123", NewPassword = "passwordabc", ConfirmPassword = "passwordabc" };
			var jsonPasswordDto = JsonConvert.SerializeObject(passwordDto);
			var content = new StringContent(jsonPasswordDto, Encoding.UTF8, "application/json");

			var token = await GetToken();
			HttpRequestMessage request = new(HttpMethod.Post, $"/api/users/ChangePassword");
			request.Headers.Add("Authorization", "Bearer " + token);
			request.Content = content;

			// Act
			var response = await _client.SendAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
			var retResule = await response.Content.ReadFromJsonAsync<ReturnModel>();
			response.StatusCode.Equals(HttpStatusCode.OK);
			retResule?.IsSuccess.Should().BeTrue();
		}
		[Fact, TestPriority(7)]
		public async Task ChangePassword_Returns_UnAuthorized()
		{
			using (var scope = _factory.Services.CreateScope())
			{
				var scopeServices = scope.ServiceProvider;
				var db = scopeServices.GetRequiredService<ApplicationDbContext>();

				db.Database.EnsureCreated();
				db.Database.Migrate();
			}
			// Arrange
			var passwordDto = new PasswordDto { Id = 5, CurrentPassword = "password123", NewPassword = "passwordabc", ConfirmPassword = "passwordabc" };
			var jsonPasswordDto = JsonConvert.SerializeObject(passwordDto);
			var content = new StringContent(jsonPasswordDto, Encoding.UTF8, "application/json");

			var token = await GetToken();
			HttpRequestMessage request = new(HttpMethod.Post, $"/api/users/ChangePassword");
			request.Content = content;

			// Act
			var response = await _client.SendAsync(request);
			
			// Assert
			response.Should().NotBeNull();
			response.StatusCode.Should().NotBe(HttpStatusCode.OK);
			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}
	}
}
