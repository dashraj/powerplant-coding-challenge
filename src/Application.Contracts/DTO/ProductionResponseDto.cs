
using System.Text.Json.Serialization;

namespace Application.Contracts.DTO
{
    public class ProductionResponseDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("p")]
        public double Power { get; set; }
    }
}
