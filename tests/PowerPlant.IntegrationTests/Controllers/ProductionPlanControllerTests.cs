using System.Text;
using System.Text.Json;
using Application.Contracts.DTO;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PowerPlant.IntegrationTests.Controllers
{
    public class ProductionPlanControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        public ProductionPlanControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task PostAsync_ValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            // Read request and response JSON from file
            var requestJson = File.ReadAllText("payloads/payload3.json");
            var responseJson = File.ReadAllText("payloads/response3.json");

            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/productionplan", content);

            // Assert
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var productionResponse = JsonSerializer.Deserialize<List<ProductionResponseDto>>(responseContent);
            var expectedResponse = JsonSerializer.Deserialize<List<ProductionResponseDto>>(responseJson);

            Assert.NotNull(productionResponse);
            Assert.True(productionResponse.Any());
            Assert.Equal(productionResponse.Count(), expectedResponse?.Count()); 
            for ( var i = 0; i < productionResponse.Count; i++)
            {
                Assert.Equal(productionResponse[i].Name, expectedResponse?[i].Name);
                Assert.Equal(productionResponse[i].Power, expectedResponse?[i].Power);
            }
            
        }
    }
}