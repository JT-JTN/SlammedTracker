using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ST.Core;
using ST.Infrastructure.Plugins;

namespace ST.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly ILogger<ColorsController> _logger;
        private readonly IColorRepository _repo;

        public ColorsController(ILogger<ColorsController> logger,
                                    IColorRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var colors = await _repo.GetAllColorsAsync();
                if (colors == null || !colors.Any())
                {
                    _logger.LogWarning("No Colors Found In Database");
                    return NotFound();
                }
                _logger.LogInformation("Colors retrieved successfully");
                return Ok(colors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving colors");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var color = await _repo.GetColorByIdAsync(id);
            if (color == null || color.Id == 0)
            {
                _logger.LogWarning($"Color with id {id} not found");
                return NotFound();
            }

            return Ok(color);
        }

        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] STColor color)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad Color Data");
            
            await _repo.AddColorAsync(color);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateColor(int id, [FromBody] STColor color)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad Color Data");

            await _repo.EditColorAsync(id, color);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad Color data");

            await _repo.DeleteColorAsync(id);

            return NoContent();
        }
    }
}
