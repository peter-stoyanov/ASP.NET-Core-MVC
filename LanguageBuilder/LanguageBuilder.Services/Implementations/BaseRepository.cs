using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Data.Services
{
    public abstract class BaseRepository<TEntity, TKey>
        : IRepository<TEntity, TKey>, IAsyncRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IComparable<TKey>
    {
        private readonly LanguageBuilderDbContext _db;

        public BaseRepository(LanguageBuilderDbContext context)
        {
            _db = context;
        }

        public virtual void Add(TEntity entity)
        {
            _db.Set<TEntity>().AddAsync(entity);
            _db.SaveChangesAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public virtual TEntity Delete(TKey id)
        {
            var entity = this.Get(id);

            if (entity == null)
            {
                return null;
            }

            _db.Set<TEntity>().Remove(entity);

            _db.SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(TKey id)
        {
            var entity = await this.GetAsync(id);

            if (entity == null)
            {
                return null;
            }

            _db.Set<TEntity>().Remove(entity);

            await _db.SaveChangesAsync();

            return entity;
        }

        public virtual bool Exist(TKey id)
        {
            return this.Get(id) != null;
        }

        public virtual async Task<bool> ExistAsync(TKey id)
        {
            return await this.GetAsync(id) != null;
        }

        public virtual TEntity Get(TKey id)
        {
            return _db
                .Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id.CompareTo(id) == 0);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAll(TKey[] ids)
        {
            return _db
                .Set<TEntity>()
                .AsNoTracking()
                .Where(e => ids.Contains(e.Id));
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => _db
                .Set<TEntity>()
                .AsNoTracking());
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync(TKey[] ids)
        {
            return await Task.Run(() => _db
                .Set<TEntity>()
                .AsNoTracking()
                .Where(e => ids.Contains(e.Id)));
        }

        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            return await _db
                .Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.CompareTo(id) == 0);
        }

        public virtual void Update(TEntity modifiedentity)
        {
            _db.Set<TEntity>().Update(modifiedentity);
            _db.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity modifiedentity)
        {
            _db.Set<TEntity>().Update(modifiedentity);
            await _db.SaveChangesAsync();
        }
    }
}
