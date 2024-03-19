namespace WeatherDDD.SharedKernel.Interfaces;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
