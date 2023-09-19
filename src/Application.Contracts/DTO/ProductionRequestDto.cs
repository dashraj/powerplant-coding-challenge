namespace Application.Contracts.DTO
{
    public class ProductionRequestDto
    {
        public double Load { get; set; }
        public FuelDto? Fuels { get; set; }
        public List<PowerPlantDto>? Powerplants { get; set; }
    }
}
