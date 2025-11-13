using AutoMapper;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Business.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/goals")]
[ApiController]
public class GoalController : ControllerBase
{
    private readonly ICreatable<Goal> _creator;
    private readonly IReadable<Goal> _reader;
    private readonly IUpdatable<Goal> _updater;
    private readonly IDeletable<Goal> _deleter;
    private readonly IMapper _mapper;

    public GoalController(
        ICreatable<Goal> creator,
        IReadable<Goal> reader,
        IUpdatable<Goal> updater,
        IDeletable<Goal> deleter,
        IMapper mapper)
    {
        _creator = creator;
        _reader = reader;
        _updater = updater;
        _deleter = deleter;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Goal goal)
    {
        try
        {
            await _creator.CreateEntityAsync(_mapper.Map<Goal>(goal));
            return Ok("Goal created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Goal Create] Error creating goal: {ex}");
            return BadRequest($"Error creating goal: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var goals = await _reader.GetAllEntitiesAsync();
            return Ok(goals);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Goal GetAll] Error retrieving goals: {ex}");
            return StatusCode(500, $"Error retrieving goals: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var goal = await _reader.GetEntityByIdAsync(id);
            if (goal == null)
                return NotFound($"Goal with ID {id} not found.");
            return Ok(goal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Goal GetById] Error retrieving goal: {ex}");
            return StatusCode(500, $"Error retrieving goal: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Goal goal)
    {
        try
        {
            if (id != goal.Id)
                return BadRequest("ID in URL does not match ID in body.");

            var updated = await _updater.UpdateEntityAsync(goal);
            if (!updated)
                return NotFound($"Goal with ID {id} not found.");

            return Ok("Goal updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Goal Update] Error updating goal: {ex}");
            return StatusCode(500, $"Error updating goal: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deleted = await _deleter.DeleteEntityAsync(id);
            if (!deleted)
                return NotFound($"Goal with ID {id} not found.");

            return Ok("Goal deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Goal Delete] Error deleting goal: {ex}");
            return StatusCode(500, $"Error deleting goal: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}