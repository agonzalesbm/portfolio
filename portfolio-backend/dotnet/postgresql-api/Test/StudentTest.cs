using Xunit;
using Moq;
using AutoFixture;
using postgresql_api.Repository;
using postgresql_api.Models;
using postgresql_api.Services;
using postgresql_api.Db;
using postgresql_api.Controllers;
using Moq.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace postgresql_api.Test;

public class StudentTest
{
  private readonly Fixture _fixture;
  private readonly Mock<AppDbContext> _context;
  private readonly StudentRepository _repository;
  private readonly StudentService _service;
  private readonly DbContextOptions<AppDbContext> _options;

  public StudentTest()
  {
    _options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
      .Options;
    _fixture = new Fixture();
    _context = new Mock<AppDbContext>(_options);
    _repository = new StudentRepository(_context.Object);
    _service = new StudentService(_repository);
  }

  [Fact]
  public void PassingTest()
  {
    /* var students = _fixture.CreateMany<Student>().AsQueryable().BuildMockDbSet(); */
    var students = _fixture.Create<List<Student>>();
    Console.WriteLine("Size: " + students.Count);
    Console.WriteLine(students[0].Name);

    _context.SetupSequence(x => x.Set<Student>()).ReturnsDbSet(students);

    var _repository = new StudentRepository(_context.Object);
    var _service = new StudentService(_repository);
    var getStudents = _service.GetAll();

    Assert.NotNull(getStudents);
  }

  private static IEnumerable<Student> GetDummyStudentsList()
  {
    return new List<Student>()
    {
      new Student
      {
        Id = 1,
        Name = "Juan",
        FathersName = "Perez",
        MothersName = "Ochoa",
        BornDate = new DateTime(2024, 1, 29)
      },
      new Student
      {
        Id = 2,
        Name = "John",
        FathersName = "Doe",
        MothersName = "Luck",
        BornDate = new DateTime(2024, 1, 29)
      }
    };
  }
}
