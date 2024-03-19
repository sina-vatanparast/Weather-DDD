using WeatherDDD.SharedKernel.Interfaces;

namespace WeatherForecastPublic.Infrastructure.Data
{

    public class BaseRepository<T>(AppDbContext dbContext) : IRepository<T>
        where T : class, IAggregateRoot
    {
        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChangesAsync();
        }
    }
}
