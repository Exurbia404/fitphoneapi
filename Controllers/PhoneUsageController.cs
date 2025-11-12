using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitphoneBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneUsageController : ControllerBase
    {
        private readonly ICreatable<PhoneUsage> _creator;
        private readonly IReadable<PhoneUsage> _reader;
        private readonly IUpdatable<PhoneUsage> _updater;
        private readonly IDeletable<PhoneUsage> _deleter;

        public PhoneUsageController(
            ICreatable<PhoneUsage> creator,
            IReadable<PhoneUsage> reader,
            IUpdatable<PhoneUsage> updater,
            IDeletable<PhoneUsage> deleter)
        {
            _creator = creator;
            _reader = reader;
            _updater = updater;
            _deleter = deleter;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PhoneUsage phoneUsage) =>
            Ok(await _creator.CreateEntityAsync(phoneUsage));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _reader.GetAllEntitiesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var phoneUsage = await _reader.GetEntityByIdAsync(id);
            return phoneUsage == null ? NotFound() : Ok(phoneUsage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PhoneUsage phoneUsage)
        {
            if (id != phoneUsage.Id) return BadRequest("ID mismatch");
            var updated = await _updater.UpdateEntityAsync(phoneUsage);
            return updated ? Ok(phoneUsage) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _deleter.DeleteEntityAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}