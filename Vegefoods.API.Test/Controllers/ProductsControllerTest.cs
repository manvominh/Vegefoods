using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Vegefoods.Application.Dtos;
using Vegefoods.Domain.Entities;


namespace Vegefoods.API.Test.Controllers
{
	public class ProductsControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly CustomWebApplicationFactory<Program> _factory;
		private readonly HttpClient _client;
		public ProductsControllerTest(CustomWebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_client = _factory.CreateClient();
		}
		[Fact]
		public async Task GetProducts_ReturnsOk()
		{
			var response = await _client.GetAsync("/api/products");
			response.EnsureSuccessStatusCode();
			response.StatusCode.Equals(HttpStatusCode.OK);
		}
		[Fact]
		public async Task GetProduct_ReturnsOk()
		{
			// Arrange
			var productId = 1;
			// Act
			var response = await _client.GetAsync($"/api/products/{productId}");
			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ProductDto>();
			// Assert
			response.StatusCode.Equals(HttpStatusCode.OK);
			result.Should().NotBeNull();
			result?.Name.Should().NotBeNullOrEmpty();			
		}
	}
}
