
using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Vegefoods.Application.Dtos;
using Vegefoods.Domain.Entities;
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
			var newUser = new UserRequestDto { Email= "test@test.com", Password = "testing" };

			// Act
			var response = await _client.PostAsync("/api/users/register", new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));

			// Assert
			response.Should().NotBeNull();
			response.EnsureSuccessStatusCode();
			var userRequestDto = await response.Content.ReadFromJsonAsync<UserRequestDto>();
			userRequestDto.Should().NotBeNull();
			userRequestDto?.Email.Should().NotBeNull();
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
			var loginUser = new UserRequestDto { Email = "testlogin@test.com", Password = "123" };

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
			var loginUser = new UserRequestDto { Email = "testlogin01@test.com", Password = "123" };

			// Act
			var response = await _client.PostAsync("/api/users/login", new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json"));

			// Assert
			response.EnsureSuccessStatusCode();
			var tokenObject = await response.Content.ReadFromJsonAsync<Tokens>();
			tokenObject?.Token.Should().BeNull();
			tokenObject?.IsSuccess.Should().BeFalse();
			tokenObject.Should().NotBeNull();
		}
		private async Task<string?> GetToken()
		{
			var loginUser = new UserRequestDto { Email = "tester@test.com", Password = "123" };

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
			var userDto = await response.Content.ReadFromJsonAsync<UserDto>();
			response.Should().NotBeNull();
			userDto.Should().NotBeNull();					
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
			}
			// Arrange
			var passwordDto = new PasswordDto { Id = 2, CurrentPassword = "123", NewPassword = "abc", ConfirmPassword = "abc" };
			var jsonPasswordDto = JsonConvert.SerializeObject(passwordDto);
			var content = new StringContent(jsonPasswordDto, Encoding.UTF8, "application/json");

			var token = await GetToken();
			HttpRequestMessage request = new (HttpMethod.Post, $"/api/users/ChangePassword");
			request.Headers.Add("Authorization", "Bearer " + token);
			request.Content = content;

			// Act
			var response = await _client.SendAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();
			var isSuccess = await response.Content.ReadFromJsonAsync<bool>();
			response.StatusCode.Equals(HttpStatusCode.OK);
			isSuccess.Should().BeTrue();
		}
		[Fact, TestPriority(6)]
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
			var passwordDto = new PasswordDto { Id = 2, CurrentPassword = "123", NewPassword = "abc", ConfirmPassword = "abc" };
			var jsonPasswordDto = JsonConvert.SerializeObject(passwordDto);
			var content = new StringContent(jsonPasswordDto, Encoding.UTF8, "application/json");

			var token = await GetToken();
			HttpRequestMessage request = new(HttpMethod.Post, $"/api/users/ChangePassword");			
			request.Content = content;

			// Act
			var response = await _client.SendAsync(request);

			// Assert
			response.Should().NotBeNull();
			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}
	}
}
