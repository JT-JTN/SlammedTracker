using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST.Core;
using ST.DataAccess;
using ST.Infrastructure.Plugins;

namespace ST.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IVehicleRepository _repo;

        public VehiclesController(ILogger<VehiclesController> logger,
                                    IVehicleRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var vehicles = await _repo.GetAllVehiclesAsync();
                if (vehicles == null || !vehicles.Any())
                {
                    _logger.LogWarning("No Vehicles Found In Database");
                    return NotFound();
                }
                _logger.LogInformation("Vehicles retrieved successfully");
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving vehicles");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] STVehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid vehicle data.");

            await _repo.AddVehicleAsync(vehicle);

            return NoContent();
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await _repo.GetVehicleByIdAsync(id);
            return Ok(vehicle);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditVehicle(int id, [FromBody] STVehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid vehicle data.");

            if (id != vehicle.Id)
                return BadRequest("Vehicle ID mismatch.");

            await _repo.EditVehicleAsync(id, vehicle);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad vehicle data");
            await _repo.DeleteVehicleAsync(id);

            return NoContent();
        }
    }
}
