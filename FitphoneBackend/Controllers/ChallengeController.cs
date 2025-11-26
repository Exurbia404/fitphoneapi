using AutoMapper;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Business.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/challenges")]
[ApiController]
public class ChallengeController : ControllerBase
{
    private readonly ICreatable<Challenge> _creator;
    private readonly IReadable<Challenge> _reader;
    private readonly IUpdatable<Challenge> _updater;
    private readonly IDeletable<Challenge> _deleter;
    private readonly IMapper _mapper;

    public ChallengeController(
        ICreatable<Challenge> creator,
        IReadable<Challenge> reader,
        IUpdatable<Challenge> updater,
        IDeletable<Challenge> deleter,
        IMapper mapper)
    {
        _creator = creator;
        _reader = reader;
        _updater = updater;
        _deleter = deleter;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Challenge challengeDto)
    {
        try
        {
            await _creator.CreateEntityAsync(_mapper.Map<Challenge>(challengeDto));
            return Ok("Challenge created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Challenge Create] Error creating challenge: {ex}");
            return BadRequest($"Error creating challenge: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var challenges = await _reader.GetAllEntitiesAsync();
            return Ok(challenges);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Challenge GetAll] Error retrieving challenges: {ex}");
            return StatusCode(500, $"Error retrieving challenges: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var challenge = await _reader.GetEntityByIdAsync(id);
            if (challenge == null)
                return NotFound($"Challenge with ID {id} not found.");
            return Ok(challenge);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Challenge GetById] Error retrieving challenge: {ex}");
            return StatusCode(500, $"Error retrieving challenge: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Challenge challenge)
    {
        try
        {
            if (id != challenge.Id)
                return BadRequest("ID in URL does not match ID in body.");

            var updated = await _updater.UpdateEntityAsync(challenge);
            if (!updated)
                return NotFound($"Challenge with ID {id} not found.");

            return Ok(challenge);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Challenge Update] Error updating challenge: {ex}");
            return StatusCode(500, $"Error updating challenge: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deleted = await _deleter.DeleteEntityAsync(id);
            if (!deleted)
                return NotFound($"Challenge with ID {id} not found.");

            return Ok("Challenge deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Challenge Delete] Error deleting challenge: {ex}");
            return StatusCode(500, $"Error deleting challenge: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}