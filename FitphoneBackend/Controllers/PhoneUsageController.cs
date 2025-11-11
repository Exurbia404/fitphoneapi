using AutoMapper;
using FitphoneBackend.Business.DTOs.PhoneUsage;
using FitPhoneBackend.Business.Entities;
using FitPhoneBackend.Business.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PhoneUsageController : ControllerBase
{
    private readonly IPhoneUsageService _service;
    private readonly IMapper _mapper;

    public PhoneUsageController(IPhoneUsageService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var usage = await _service.GetPhoneUsageByUserIdAsync(userId);
        if (usage == null) return NotFound();

        return Ok(_mapper.Map<PhoneUsageDto>(usage));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usages = await _service.GetAllPhoneUsageAsync();
        return Ok(_mapper.Map<IEnumerable<PhoneUsageDto>>(usages));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PhoneUsageCreateDto dto)
    {
        var entity = _mapper.Map<PhoneUsage>(dto);
        var created = await _service.CreatePhoneUsageAsync(entity);

        return CreatedAtAction(nameof(Get), new { userId = created.UserId }, _mapper.Map<PhoneUsageDto>(created));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PhoneUsageDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch");

        var exists = await _service.GetPhoneUsageByIdAsync(id);
        if (exists == null) return NotFound();

        _mapper.Map(dto, exists);
        await _service.UpdatePhoneUsageAsync(exists);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var exists = await _service.GetPhoneUsageByIdAsync(id);
        if (exists == null) return NotFound();

        await _service.DeletePhoneUsageAsync(id);
        return NoContent();
    }
}
