using Microsoft.AspNetCore.Mvc;
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Services;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Business.DTOs.UserDto;
using AutoMapper;

namespace FitPhoneBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICreatable<User> _creator;
        private readonly IReadable<User> _reader;
        private readonly IUpdatable<User> _updater;
        private readonly IDeletable<User> _deleter;
        private readonly IMapper _mapper;

        public UsersController(
            ICreatable<User> creator,
            IReadable<User> reader,
            IUpdatable<User> updater,
            IDeletable<User> deleter,
            IMapper mapper)
        {
            _creator = creator;
            _reader = reader;
            _updater = updater;
            _deleter = deleter;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userDTO)
        {
            try
            {
                await _creator.CreateEntityAsync(_mapper.Map<User>(userDTO));
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                Console.WriteLine($"[Create] Error creating user: {ex}");
                return BadRequest($"Error creating user: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _reader.GetAllEntitiesAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAll] Error retrieving users: {ex}");
                return StatusCode(500, $"Error retrieving users: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _reader.GetEntityByIdAsync(id);
                if (user == null)
                    return NotFound($"User with ID {id} not found.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetById] Error retrieving user: {ex}");
                return StatusCode(500, $"Error retrieving user: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User user)
        {
            try
            {
                if (id != user.Id)
                    return BadRequest("ID in URL does not match ID in body.");

                var updated = await _updater.UpdateEntityAsync(user);
                if (!updated)
                    return NotFound($"User with ID {id} not found.");

                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Update] Error updating user: {ex}");
                return StatusCode(500, $"Error updating user: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _deleter.DeleteEntityAsync(id);
                if (!deleted)
                    return NotFound($"User with ID {id} not found.");

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Delete] Error deleting user: {ex}");
                return StatusCode(500, $"Error deleting user: {ex.Message}");
            }
        }
    }
}
