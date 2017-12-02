using LanguageBuilder.Data.Models;
using System;
using System.Linq;

namespace LanguageBuilder.Services.Contracts
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IComparable<TKey>
    {
        void Add(TEntity entity);

        TEntity Get(TKey id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(TKey[] ids);

        void Update(TEntity modifiedType);

        TEntity Delete(TKey id);

        bool Exist(TKey id);
    }
}
