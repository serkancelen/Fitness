﻿using Fitness.Entities.Dto;
using Fitness.Entities.RequestFeatures;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Authorize]
[Route("api/nutrition")]
[ApiController]
public class NutritionController : ControllerBase
{
    private readonly INutritionService _nutritionService;

    public NutritionController(INutritionService nutritionService)
    {
        _nutritionService = nutritionService;
    }

    [HttpGet("get/{userId}")]
    public async Task<IActionResult> GetNutritionEntries([FromQuery]FitnessParameters fitnessParameters,int userId)
    {
        var response = await _nutritionService.GetNutritionByUserIdAsync(fitnessParameters,userId);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"GetNutritionEntries failed for UserId {userId}: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateNutritionEntry([FromBody] NutritionDto nutritionEntryDto)
    {
        var response = await _nutritionService.CreateNutritionAsync(nutritionEntryDto);

        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"CreateNutritionEntry failed: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateNutritionEntry(int id, [FromBody] NutritionDto nutritionEntryDto)
    {
        var response = await _nutritionService.UpdateNutritionAsync(id, nutritionEntryDto);

        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"UpdateNutritionEntry failed for NutritionId {id}: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteNutritionEntry(int id)
    {
        var response = await _nutritionService.DeleteNutritionAsync(id);

        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"DeleteNutritionEntry failed for NutritionId {id}: {response.Message}");

        return BadRequest(response.Message);
    }
}
