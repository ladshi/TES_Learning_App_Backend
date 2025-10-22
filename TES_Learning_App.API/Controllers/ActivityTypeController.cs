﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TES_Learning_App.Application_Layer.DTOs.ActivityType.Requests;
using TES_Learning_App.Application_Layer.DTOs.ActivityType.Response;
using TES_Learning_App.Application_Layer.Interfaces.IServices;

namespace TES_Learning_App.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActivityTypesController : BaseApiController
    {
        private readonly IActivityTypeService _activityTypeService;

        public ActivityTypesController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityTypeDto>>> GetAll()
        {
            return Ok(await _activityTypeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityTypeDto>> GetById(int id)
        {
            var activityType = await _activityTypeService.GetByIdAsync(id);
            if (activityType == null) return NotFound();
            return Ok(activityType);
        }

        [HttpPost]
        public async Task<ActionResult<ActivityTypeDto>> Create(CreateActivityTypeDto dto)
        {
            var newActivityType = await _activityTypeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newActivityType.Id }, newActivityType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateActivityTypeDto dto)
        {
            await _activityTypeService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _activityTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
