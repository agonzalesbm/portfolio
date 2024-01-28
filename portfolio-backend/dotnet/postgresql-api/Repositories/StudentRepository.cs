using Microsoft.EntityFrameworkCore;
using postgresql_api.Db;
using postgresql_api.Models;

namespace postgresql_api.Repository;

public class StudentRepository : IStudentRepository
{
  private readonly AppDbContext _context;

  public StudentRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Student>> GetAll()
  {
    return await _context.Students.ToListAsync();
  }

  public async Task<Student?> Get(int id)
  {
    var studentItem = await _context.Students.FindAsync(id);
    if (studentItem is null)
    {
      throw new Exception();
    }
    return studentItem;
  }

  public async Task<Student> Create(Student student)
  {
    var createStudent = await _context.AddAsync(student);
    await _context.SaveChangesAsync();
    return createStudent.Entity;
  }

  public async Task Update(int id, Student student)
  {
    var studentItem = await _context.Students.FindAsync(id);
    if (studentItem is null)
    {
      throw new Exception();
    }

    studentItem.Id = student.Id;
    studentItem.Name = student.Name;
    studentItem.BornDate = student.BornDate;
    studentItem.FathersName = student.FathersName;
    studentItem.MothersName = studentItem.MothersName;

    // WARNING: Improve the exceptions
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      throw new Exception();
    }
  }

  public async Task Delete(int id)
  {
    var studentItem = await _context.Students.FindAsync(id);
    if (studentItem is null)
    {
      throw new Exception();
    }

    try
    {
      _context.Students.Remove(studentItem);
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      throw new Exception();
    }
  }
}
