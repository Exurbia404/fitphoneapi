using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitphoneBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly ICreatable<Education> _creator;
        private readonly IReadable<Education> _reader;
        private readonly IUpdatable<Education> _updater;
        private readonly IDeletable<Education> _deleter;

        public EducationController(
            ICreatable<Education> creator,
            IReadable<Education> reader,
            IUpdatable<Education> updater,
            IDeletable<Education> deleter)
        {
            _creator = creator;
            _reader = reader;
            _updater = updater;
            _deleter = deleter;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Education education) =>
            Ok(await _creator.CreateEntityAsync(education));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _reader.GetAllEntitiesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var education = await _reader.GetEntityByIdAsync(id);
            return education == null ? NotFound() : Ok(education);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Education education)
        {
            if (id != education.Id) return BadRequest("ID mismatch");
            var updated = await _updater.UpdateEntityAsync(education);
            return updated ? Ok(education) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _deleter.DeleteEntityAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}