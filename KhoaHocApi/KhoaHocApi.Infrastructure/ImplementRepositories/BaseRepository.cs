using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Infrastructure.ImplementRepositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected IDbContext _IDbContext = null;
        protected DbSet<TEntity> _dbSet;
        protected DbContext _dbContext;
        protected DbSet<TEntity> DbSet
        {
            get
            {
                if(_dbSet == null)
                {
                    _dbSet = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }
                return _dbSet;
            }
        }
        public BaseRepository(IDbContext dbContext)
        {
            _IDbContext= dbContext;
            _dbContext = (DbContext) dbContext;
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? DbSet.Where(expression) : DbSet;
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(string include, Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrEmpty(include))
            {
                query = BuildQueryable(new List<string> { include}, expression);
                return await query.CountAsync();
            }
            return await CountAsync(expression);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _IDbContext.CommitChangeAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            await _IDbContext.CommitChangeAsync();
            return entities;
        }

        public async Task DeleteAsync(long id)
        {
            var dataEntity = await DbSet.FindAsync(id);
            if(dataEntity != null)
            {
                DbSet.Remove(dataEntity);
                await _IDbContext.CommitChangeAsync();
            }
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = expression != null ? DbSet.Where(expression) : DbSet;
            var dataQuery = query;
            if(dataQuery != null)
            {
                DbSet.RemoveRange(dataQuery);
                await _IDbContext.CommitChangeAsync();
            }
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null) // Lấy tất cả dữ liệu theo điều kiện
        {
            IQueryable<TEntity> query = expression != null ? DbSet.Where(expression) : DbSet;
            return query;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression) // Lấy dữ liệu theo điều kiện
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> GetByIdAsync(long id) // Lấy dữ liệu theo khóa chính
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression = null) 
        {
            return await DbSet.FirstOrDefaultAsync(expression);

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified; // cập nhật thông tin từ 1 hoặc nhiều thằng
            await _IDbContext.CommitChangeAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _IDbContext.CommitChangeAsync();
            return entities;
        }


        protected IQueryable<TEntity> BuildQueryable(List<string> includes, Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if(includes != null && includes.Count > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include.Trim());
                }
            }
            return query;
        } 
    }
}
