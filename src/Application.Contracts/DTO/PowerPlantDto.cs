using Core.Enums;
using System.Text.Json.Serialization;

namespace Application.Contracts.DTO
{
    public class PowerPlantDto
    {
        public string? Name { get; set; }

        public PowerPlantType Type { get; set; }

        public double Efficiency { get; set; }

        [JsonPropertyName("pmin")]
        public double PowerMinimum { get; set; }

        [JsonPropertyName("pmax")]
        public double PowerMaximum { get; set; }
    }
}
