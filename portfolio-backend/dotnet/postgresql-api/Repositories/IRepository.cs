namespace postgresql_api.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(int id);
    Task<T> Create(T entity);
    Task Update(int id, T entity);
    Task Delete(int id);
}
