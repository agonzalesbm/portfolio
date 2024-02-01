using postgresql_api.Models;
using postgresql_api.Repository;

namespace postgresql_api.Services;

public class SubjectService : ISubjectService
{
  private readonly ISubjectRepository _repository;

  public SubjectService(ISubjectRepository repository)
  {
    _repository = repository;
  }

  public async Task<IEnumerable<Subject>> GetAll()
  {
    return await _repository.GetAll();
  }
  public async Task<Subject?> Get(int id)
  {
    return await _repository.Get(id);
  }

  public async Task<Subject> Create(Subject subject)
  {
    return await _repository.Create(subject);
  }

  public async Task Update(int id, Subject subject)
  {
    await _repository.Update(id, subject);
  }

  public async Task Delete(int id)
  {
    await _repository.Delete(id);
  }
}
