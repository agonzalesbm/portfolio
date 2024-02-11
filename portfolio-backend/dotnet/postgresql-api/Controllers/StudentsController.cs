using Microsoft.AspNetCore.Mvc;
using postgresql_api.Models;
using postgresql_api.Services;

namespace postgresql_api.Controllers;

[Route("postgresql_api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll()
    {
        var students = await _service.GetAll();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> Get(int id)
    {
        var student = await _service.Get(id);
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Create(Student student)
    {
        var studentItem = await _service.Create(student);
        return CreatedAtAction(nameof(Get), new { id = studentItem.Id }, studentItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Student student)
    {
        await _service.Update(id, student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

