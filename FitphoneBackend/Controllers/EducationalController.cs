// EducationController.cs
using FitphoneBackend.Business.Services;
using FitPhoneBackend.Business.Entities;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EducationController : ControllerBase
{
    private readonly EducationService _educationService;

    public EducationController(EducationService educationService)
    {
        _educationService = educationService;
    }

    // GET: api/Education
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Education>>> GetEducations()
    {
        var educations = await _educationService.GetAllAsync();
        return Ok(educations);
    }

    // GET: api/Education/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Education>> GetEducation(int id)
    {
        var education = await _educationService.GetByIdAsync(id);
        if (education == null)
        {
            return NotFound();
        }
        return Ok(education);
    }

    // POST: api/Education
    [HttpPost]
    public async Task<ActionResult<Education>> CreateEducation([FromBody] Education education)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdEducation = await _educationService.CreateAsync(education);
        return CreatedAtAction(nameof(GetEducation), new { id = createdEducation.Id }, createdEducation);
    }

    // PUT: api/Education/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEducation(int id, [FromBody] Education updatedEducation)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _educationService.UpdateAsync(id, updatedEducation);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // DELETE: api/Education/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducation(int id)
    {
        try
        {
            await _educationService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}