
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Vegefoods.Application.Dtos;
using Vegefoods.Domain.Entities;

namespace Vegefoods.API.Test.Controllers
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class TestPriorityAttribute : Attribute
	{
		public TestPriorityAttribute(int priority)
		{
			Priority = priority;
		}

		public int Priority { get; private set; }
	}
	public class UsersControllerTest : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly WebApplicationFactory<Program> _factory;
		private readonly HttpClient _client;
		public UsersControllerTest(WebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_client = _factory.CreateClient();
		}
		[Fact, TestPriority(1)]
		public async Task Register_ReturnsOk()
		{
			// Arrange
			var newUser = new UserRequestDto { Email= "test@test.com", Password = "testing" };

			// Act
			var response = await _client.PostAsync("/api/users/register", new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));

			// Assert
			response.EnsureSuccessStatusCode();
			var userRequestDto = await response.Content.ReadFromJsonAsync<UserRequestDto>();
			Assert.NotNull(userRequestDto);
		}
		[Fact, TestPriority(1)]
		public async Task Login_ReturnsOk()
		{
			// Arrange
			var loginUser = new UserRequestDto { Email = "test@test.com", Password = "testing" };

			// Act
			var response = await _client.PostAsync("/api/users/login", new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json"));

			// Assert
			response.EnsureSuccessStatusCode();
			var tokenObject = await response.Content.ReadFromJsonAsync<Tokens>();
			Assert.NotNull(tokenObject);
		}
	}
}
