using postgresql_api.Models;
using postgresql_api.Repository;

namespace postgresql_api.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repostory)
    {
        _repository = repostory;
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Student?> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<Student> Create(Student student)
    {
        return await _repository.Create(student);
    }

    public async Task Update(int id, Student student)
    {
        await _repository.Update(id, student);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}
