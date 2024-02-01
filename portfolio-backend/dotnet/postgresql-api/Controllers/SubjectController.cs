using postgresql_api.Models;
using postgresql_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace postgresql_api.Repository;

[Route("postgresql_api/[controller]")]
[ApiController]
public class SubjectController : ControllerBase
{
  private readonly ISubjectService _service;

  public SubjectController(ISubjectService service)
  {
    _service = service;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Subject>>> GetAll()
  {
    var subjects = await _service.GetAll();
    return Ok(subjects);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Subject>> Get(int id)
  {
    var student = await _service.Get(id);
    return Ok(student);
  }

  [HttpPost]
  public async Task<ActionResult<Subject>> Create(Subject subject)
  {
    var subjectItem = await _service.Create(subject);
    return CreatedAtAction(nameof(Get), new { id = subject.Id }, subjectItem);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, Subject subject)
  {
    await _service.Update(id, subject);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    await _service.Delete(id);
    return NoContent();
  }
}
