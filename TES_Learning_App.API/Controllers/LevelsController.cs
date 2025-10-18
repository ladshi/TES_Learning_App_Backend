using Microsoft.AspNetCore.Mvc;
using TES_Learning_App.Application_Layer.Dtos;
using TES_Learning_App.Application_Layer.Interfaces;

namespace TES_Learning_App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly ILevelService _levelService;

        public LevelsController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LevelResponseDTO>>> GetLevels()
        {
            var levels = await _levelService.GetAllLevelsAsync();
            return Ok(levels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LevelResponseDTO>> GetLevel(int id)
        {
            var level = await _levelService.GetLevelByIdAsync(id);
            if (level == null)
                return NotFound();
                
            return Ok(level);
        }

        [HttpPost]
        public async Task<ActionResult<LevelResponseDTO>> CreateLevel(CreateLevelDTO createLevelDto)
        {
            var level = await _levelService.CreateLevelAsync(createLevelDto);
            return CreatedAtAction(nameof(GetLevel), new { id = level.Id }, level);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLevel(int id, UpdateLevelDTO updateLevelDto)
        {
            if (id != updateLevelDto.Id)
                return BadRequest("ID in the URL does not match the ID in the request body.");

            try
            {
                await _levelService.UpdateLevelAsync(updateLevelDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel(int id)
        {
            try
            {
                await _levelService.DeleteLevelAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
