using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Vegefoods.Application.Dtos;
using Vegefoods.Domain.Entities;


namespace Vegefoods.API.Test.Controllers
{
	public class ProductsControllerTest : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly WebApplicationFactory<Program> _factory;
		private readonly HttpClient _client;
		public ProductsControllerTest(WebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_client = _factory.CreateClient();
		}
		[Fact]
		public async Task GetProducts_ReturnsOk()
		{
			var response = await _client.GetAsync("/api/products");
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
		[Fact]
		public async Task GetUser_ReturnsOk()
		{
			// Arrange
			var productId = 1;
			// Act
			var response = await _client.GetAsync($"/api/products/{productId}");
			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ProductDto>();
			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

			Assert.NotNull(result);
			Assert.Equal(productId, result.Id);
		}
	}
}
