namespace postgresql_api.Services;

public interface IService<T> where T : class
{
  Task<IEnumerable<T>> GetAll();
  Task<T?> Get(int id);
  Task<T> Create(T entity);
  Task Update(int id, T entity);
  Task Delete(int id);
}
