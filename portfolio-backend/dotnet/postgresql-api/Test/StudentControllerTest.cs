using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using postgresql_api.Controllers;
using postgresql_api.Exceptions;
using postgresql_api.Models;
using postgresql_api.Repository;
using postgresql_api.Services;
using Xunit;

namespace postgresql_api.Test;

public class StudentControllerTest
{
    private readonly Fixture fixture;
    private readonly Mock<IStudentRepository> repositoryMock;
    private StudentService serviceMock;
    private StudentsController controllerMock;

    public StudentControllerTest()
    {
        fixture = new Fixture();
        repositoryMock = new Mock<IStudentRepository>();
        serviceMock = new StudentService(repositoryMock.Object);
        controllerMock = new StudentsController(serviceMock);
    }

    [Fact]
    public void GetAll_StudentListWithElements_ReturnOkObjectResultResponse()
    {
        var students = fixture.Create<List<Student>>();
        repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(students);
        serviceMock = new StudentService(repositoryMock.Object);
        controllerMock = new StudentsController(serviceMock);

        var result = controllerMock.GetAll();
        var taskResult = result.Result;
        var actionResult = taskResult.Result;
        var response = actionResult as OkObjectResult;

        repositoryMock.Verify(r => r.GetAll(), Times.Once);
        Assert.NotNull(response);
        Assert.Equal(200, response.StatusCode);
        Assert.IsType<List<Student>>(response.Value);
    }

    [Fact]
    public void Get_WhenValidId_ReturnObjectResultResponse()
    {
        var validId = 133;
        repositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync(fixture.Build<Student>()
            .With(s => s.Id, validId)
            .Create());

        var result = controllerMock.Get(validId);
        var taskResult = result.Result;
        var actionResult = taskResult.Result;
        var response = actionResult as OkObjectResult;
        var studentItem = response?.Value;

        repositoryMock.Verify(r => r.Get(validId), Times.Once);
        Assert.NotNull(response);
        Assert.Equal(200, response.StatusCode);
        Assert.NotNull(studentItem);
        Assert.IsType<Student>(studentItem);
    }

    [Fact]
    public async Task Get_WhenIdIsValid_ThrowExcetionWithCustomMessageAndStateValue()
    {
        var nonExistentId = 10;
        var msg = "Student not found.";
        var statusCode = 404;
        repositoryMock.Setup(r => r.Get(nonExistentId)).ThrowsAsync(new HttpResponseException(statusCode, msg));

        var exception = await Assert.ThrowsAsync<HttpResponseException>(async () => await controllerMock.Get(nonExistentId));

        repositoryMock.Verify(r => r.Get(nonExistentId), Times.Once);
        Assert.NotNull(exception);
        Assert.Equal(msg, exception.Value);
        Assert.Equal(statusCode, exception.StatusCode);
    }

    [Fact]
    public async Task Get_WhenIdIsLessThanZero_ThrowExceptionWithCustomMessageAndStateValue()
    {
        var invalidId = -1;
        var msg = "Invalid id, should be greather than zero";
        var statusCode = 400;
        repositoryMock.Setup(r => r.Get(invalidId)).ThrowsAsync(new HttpResponseException(statusCode, msg));

        var exception = await Assert.ThrowsAsync<HttpResponseException>(async () => await controllerMock.Get(invalidId));

        repositoryMock.Verify(r => r.Get(invalidId), Times.Once);
        Assert.NotNull(exception);
        Assert.Equal(msg, exception.Value);
        Assert.Equal(statusCode, exception.StatusCode);
    }

    [Fact]
    public async Task Get_WhenIdIsZero_ThrowExceptionWithCustomMessageAndStateValue()
    {
        var zeroId = 0;
        var msg = "Invalid id, should be greather than zero";
        var statusCode = 400;
        repositoryMock.Setup(r => r.Get(zeroId)).ThrowsAsync(new HttpResponseException(statusCode, msg));

        var exception = await Assert.ThrowsAsync<HttpResponseException>(async () => await controllerMock.Get(zeroId));

        repositoryMock.Verify(r => r.Get(zeroId), Times.Once);
        Assert.NotNull(exception);
        Assert.Equal(msg, exception.Value);
        Assert.Equal(statusCode, exception.StatusCode);
    }

    [Fact]
    public void Create_WhenValidStudent_ReturnCreatedActionResult()
    {
        var student = fixture.Create<Student>();
        repositoryMock.Setup(r => r.Create(It.IsAny<Student>())).ReturnsAsync(student);

        var result = controllerMock.Create(student);
        var taskResult = result.Result;
        var actionResult = taskResult.Result;

        repositoryMock.Verify(r => r.Create(student), Times.Once);
        Assert.NotNull(result);
        Assert.IsType<CreatedAtActionResult>(actionResult);
    }

    [Fact]
    public async Task Create_WhenNullStudent_ReturnExceptionWithBadRequestCodeStatusAndCustomMessage()
    {
        var nullStudent = (Student?)null!;
        var statusCode = 400;
        var msg = "Student not should be null";
        repositoryMock.Setup(r => r.Create(nullStudent)).ThrowsAsync(new HttpResponseException(statusCode, msg));

        var exception = await Assert.ThrowsAsync<HttpResponseException>(async () => await controllerMock.Create(nullStudent));

        repositoryMock.Verify(r => r.Create(nullStudent), Times.Once);
        Assert.NotNull(exception);
        Assert.Equal(msg, exception.Value);
        Assert.Equal(statusCode, exception.StatusCode);
    }

    [Fact]
    public void Update_WhenValidStudentAndId_CompleteTaskAfterUpdate()
    {
        var validId = fixture.Create<int>();
        var validStudent = fixture.Build<Student>()
          .With(s => s.Id, validId)
          .Create();
        repositoryMock.Setup(r => r.Update(It.IsNotNull<int>(), It.IsNotNull<Student>())).Returns(Task.CompletedTask);

        var taskResult = controllerMock.Update(validId, validStudent);

        repositoryMock.Verify(r => r.Update(validId, validStudent));
        Assert.True(taskResult.IsCompletedSuccessfully);
    }

    [Fact]
    public void Delete_WhenValidId_Complete()
    {
        var validId = fixture.Create<int>();
        repositoryMock.Setup(r => r.Delete(It.IsNotNull<int>())).Returns(Task.CompletedTask);

        var taskResult = controllerMock.Delete(validId);

        repositoryMock.Verify(r => r.Delete(validId), Times.Once);
        Assert.True(taskResult.IsCompletedSuccessfully);
    }
}
