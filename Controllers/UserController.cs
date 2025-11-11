using Microsoft.AspNetCore.Mvc;
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Services;
using FitPhoneBackend.Business.Interfaces;

namespace FitphoneBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICreatable<User> _creator;
        private readonly IReadable<User> _reader;
        private readonly IUpdatable<User> _updater;
        private readonly IDeletable<User> _deleter;

        public UsersController(
            ICreatable<User> creator,
            IReadable<User> reader,
            IUpdatable<User> updater,
            IDeletable<User> deleter)
        {
            _creator = creator;
            _reader = reader;
            _updater = updater;
            _deleter = deleter;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user) =>
            Ok(await _creator.CreateEntityAsync(user));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _reader.GetAllEntitiesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _reader.GetEntityByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User user)
        {
            if (id != user.Id) return BadRequest("ID mismatch");
            var updated = await _updater.UpdateEntityAsync(user);
            return updated ? Ok(user) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _deleter.DeleteEntityAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
