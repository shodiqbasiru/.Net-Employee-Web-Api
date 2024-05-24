using System.Linq.Expressions;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;

namespace EmployeeApi.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> SaveAsync(TEntity entity);
    TEntity Attach(TEntity entity);
    Task<TEntity?> FindByIdAsync(Guid id);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes);
    Task<List<TEntity>> FindAllAsync();
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria);
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes);
    Task<List<TEntity>> FindAllAsync(string[] includes);
    Task<PageResult<TEntity>> FindAllAsync(int page, int pageSize);
    Task<PageResult<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int page, int pageSize);
    Task<PageResult<TEntity>> FindAllAsync(int page, int pageSize, string[] includes);
    Task<PageResult<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int page, int pageSize, string[] includes);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}