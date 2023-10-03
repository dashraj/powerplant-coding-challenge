using Application.Contracts.DTO;
using Application.Contracts.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/productionplan")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanManager manager;

        public ProductionPlanController(IProductionPlanManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        public async Task<IEnumerable<ProductionResponseDto>> PostAsync([FromBody] ProductionRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadHttpRequestException("Invalid input. Model validation failed.");
            }

            if(requestDto.Load < 1) { throw new BadHttpRequestException("Load is not valid"); }
            return await manager.GetProductionPlansAsync(requestDto);
        }
    }
}