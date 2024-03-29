﻿using Fitness.Entities.Dto;
using Fitness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWorkouts()
    {
        var response = await _workoutService.GetAllWorkoutsAsync(HttpContext.User);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"GetAllWorkouts failed: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllWorkoutsByUserId(int userId)
    {
        var response = await _workoutService.GetAllWorkoutsByUserIdAsync(userId, HttpContext.User);

        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"GetAllWorkoutsByUserId failed for UserId {userId}: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkout(WorkoutDto workoutDto)
    {
        var response = await _workoutService.CreateWorkoutAsync(workoutDto);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"CreateWorkout failed: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkout(int id, WorkoutDto workoutDto)
    {
        var response = await _workoutService.UpdateWorkoutAsync(id, workoutDto);
        if (response.Success)
        {
            return Ok(response.Data);
        }

        Log.Information($"UpdateWorkout failed for WorkoutId {id}: {response.Message}");

        return BadRequest(response.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        var response = await _workoutService.DeleteWorkoutAsync(id);
        if (response.Success)
        {
            return Ok(response.Message);
        }

        Log.Information($"DeleteWorkout failed for WorkoutId {id}: {response.Message}");

        return BadRequest(response.Message);
    }
}
