﻿//using Fitness.Entities.Dto;
//using Fitness.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace Fitness.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class WorkoutController : ControllerBase
//    {
//        private readonly IWorkoutService _workoutService;

//        public WorkoutController(IWorkoutService workoutService)
//        {
//            _workoutService = workoutService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllWorkouts()
//        {
//            var response = await _workoutService.GetAllWorkoutsAsync();
//            if (response.Success)
//            {
//                return Ok(response.Data);
//            }
//            return BadRequest(response.Message);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetWorkoutById(int id)
//        {
//            var response = await _workoutService.GetAllWorkoutsByUserIdAsync(id);
//            if (response.Success)
//            {
//                return Ok(response.Data);
//            }
//            return BadRequest(response.Message);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateWorkout(WorkoutDto workoutDto)
//        {
//            var response = await _workoutService.CreateWorkoutAsync(workoutDto);
//            if (response.Success)
//            {
//                return Ok(response.Data);
//            }
//            return BadRequest(response.Message);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateWorkout(int id, WorkoutDto workoutDto)
//        {
//            var response = await _workoutService.UpdateWorkoutAsync(id, workoutDto);
//            if (response.Success)
//            {
//                return Ok(response.Data);
//            }
//            return BadRequest(response.Message);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteWorkout(int id)
//        {
//            var response = await _workoutService.DeleteWorkoutAsync(id);
//            if (response.Success)
//            {
//                return Ok(response.Message);
//            }
//            return BadRequest(response.Message);
//        }
//    }
//}