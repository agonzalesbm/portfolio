using Xunit;
using Moq;
using AutoFixture;
using postgresql_api.Repository;
using postgresql_api.Models;
using postgresql_api.Services;

namespace postgresql_api.Test;

public class StudentServiceTest
{
  private Fixture fixture;
  private Mock<IStudentRepository> repositoryMock;
  private StudentService serviceMock;

  public StudentServiceTest()
  {
    fixture = new Fixture();
    repositoryMock = new Mock<IStudentRepository>();
    serviceMock = new StudentService(repositoryMock.Object);
  }

  [Fact]
  public void GetAll_StudentListWithElements_SizeGreatherThanZero()
  {
    var students = fixture.Create<List<Student>>();
    repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(students);

    var getStudents = serviceMock.GetAll().Result;

    repositoryMock.Verify(r => r.GetAll(), Times.Once);
    Assert.NotNull(getStudents);
    Assert.True(getStudents.Count() > 0);
    Assert.IsType<List<Student>>(getStudents);
  }

  [Fact]
  public void Get_StudentListWithOneElement_StudentWithCorrectValues()
  {
    var id = 1;
    var name = "John";
    var fathersName = "Doe";
    var bornDate = new DateTime(2000, 2, 25);
    repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(new Student
    {
      Name = name,
      Id = id,
      FathersName = fathersName,
      BornDate = bornDate
    });

    var getStudent = serviceMock.Get(1).Result;

    repositoryMock.Verify(r => r.Get(1), Times.Once);
    Assert.NotNull(getStudent);
    Assert.Equal(id, getStudent.Id);
    Assert.Equal(fathersName, getStudent.FathersName);
    Assert.Equal(bornDate, getStudent.BornDate);
  }

  [Fact]
  public void Create_StudentListWithoutElements_StudentWithNotNullProperties()
  {
    var id = 24;
    var name = "Jane";
    var fathersName = "Doe";
    var bornDate = new DateTime(2001, 2, 13);

    var student = fixture.Build<Student>()
      .With(s => s.Id, id)
      .With(s => s.Name, name)
      .With(s => s.FathersName, fathersName)
      .Without(s => s.MothersName)
      .With(s => s.BornDate, bornDate)
      .Create();
    repositoryMock.Setup(r => r.Create(student)).ReturnsAsync(student);

    var getStudent = serviceMock.Create(student).Result;

    repositoryMock.Verify(r => r.Create(student), Times.Once);
    Assert.NotNull(getStudent);
    Assert.Equal(getStudent.Id, id);
    Assert.Equal(getStudent.FathersName, fathersName);
    Assert.Null(getStudent.MothersName);
    Assert.Equal(getStudent.BornDate, bornDate);
  }

  [Fact]
  public void Delete_StudentListWithElements_CompletedTaskAfterDelete()
  {
    var id = 223;
    var students = fixture.Create<List<Student>>();
    var student = fixture.Build<Student>()
      .With(s => s.Id, id)
      .Create();
    students.Append(student);
    repositoryMock.Setup(r => r.Delete(id)).Returns(Task.CompletedTask);

    var resultTask = serviceMock.Delete(id);

    repositoryMock.Verify(r => r.Delete(id), Times.Once);
    Assert.True(resultTask.IsCompletedSuccessfully);
  }

  [Fact]
  public void Update_StudentListWithElements_CompletedTaskAfterUpdate()
  {
    var id = 334;
    var students = fixture.Create<IEnumerable<Student>>();
    var student = fixture.Build<Student>()
      .With(s => s.Id, id)
      .Create();
    students.Append(student);
    repositoryMock.Setup(r => r.Update(id, student)).Returns(Task.CompletedTask);

    var resultTask = serviceMock.Update(id, student);

    repositoryMock.Verify(r => r.Update(id, student));
    Assert.True(resultTask.IsCompletedSuccessfully);
  }
}
