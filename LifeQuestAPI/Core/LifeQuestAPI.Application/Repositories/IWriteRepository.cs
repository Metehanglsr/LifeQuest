namespace LifeQuestAPI.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(List<T> entities);
    bool Remove(T entity);
    Task<bool> RemoveAsync(string id);
    bool Update(T model);
    public Task<int> SaveAsync();
}