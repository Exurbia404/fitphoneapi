using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Interfaces;
using FitPhoneBackend.Business.DTOs.PhoneUsageDto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace FitPhoneBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneUsageController : ControllerBase
    {
        private readonly ICreatable<PhoneUsage> _creator;
        private readonly IReadable<PhoneUsage> _reader;
        private readonly IUpdatable<PhoneUsage> _updater;
        private readonly IDeletable<PhoneUsage> _deleter;
        private readonly IMapper _mapper;

        public PhoneUsageController(
            ICreatable<PhoneUsage> creator,
            IReadable<PhoneUsage> reader,
            IUpdatable<PhoneUsage> updater,
            IDeletable<PhoneUsage> deleter,
            IMapper mapper)
        {
            _creator = creator;
            _reader = reader;
            _updater = updater;
            _deleter = deleter;
             _mapper = mapper;
        }

        [HttpPost]
         public async Task<IActionResult> Create(PhoneUsageDto phoneUsageDto)
        {
            try
            {
                await _creator.CreateEntityAsync(_mapper.Map<PhoneUsage>(phoneUsageDto));
                return Ok("PhoneUsage created successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                Console.WriteLine($"[Create] Error creating phone usage: {ex}");
                return BadRequest($"Error creating phone usage: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

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