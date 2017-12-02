using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Services.Contracts
{
    public interface IAsyncRepository<TEntity, TKey>
        where TEntity : Data.Models.BaseEntity<TKey>
        where TKey : IComparable<TKey>
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> GetAsync(TKey id);

        Task<IQueryable<TEntity>> GetAllAsync();

        Task<IQueryable<TEntity>> GetAllAsync(TKey[] ids);

        Task UpdateAsync(TEntity modifiedType);

        Task<TEntity> DeleteAsync(TKey id);

        Task<bool> ExistAsync(TKey id);
    }
}
