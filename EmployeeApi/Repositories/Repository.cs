using System.Linq.Expressions;
using EmployeeApi.Data;
using EmployeeApi.Models.Requests;
using EmployeeApi.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);
        return entry.Entity;
    }

    public TEntity Attach(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Attach(entity);
        return entry.Entity;
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(criteria);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes)
    {
        var query = GetQueryable(includes);

        return await query.FirstOrDefaultAsync(criteria);
    }

    public async Task<List<TEntity>> FindAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria)
    {
        return await _context.Set<TEntity>().Where(criteria).ToListAsync();
    }

    public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes)
    {
        GetQueryable(includes);
        return await _context.Set<TEntity>().Where(criteria).ToListAsync();
    }

    public Task<List<TEntity>> FindAllAsync(string[] includes)
    {
        return GetQueryable(includes).ToListAsync();
    }

    public async Task<PageResult<TEntity>> FindAllAsync(int page, int pageSize)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PageResult<TEntity>
        {
            TotalItems = total,
            CurrentPage = page,
            PageSize = pageSize,
            Content = data
        };
    }

    public async Task<PageResult<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int page, int pageSize)
    {
        var query = _context.Set<TEntity>().Where(criteria);
        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PageResult<TEntity>
        {
            TotalItems = total,
            CurrentPage = page,
            PageSize = pageSize,
            Content = data
        };
    }

    public async Task<PageResult<TEntity>> FindAllAsync(int page, int pageSize, string[] includes)
    {
        var query = GetQueryable(includes);
        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PageResult<TEntity>
        {
            TotalItems = total,
            CurrentPage = page,
            PageSize = pageSize,
            Content = data
        };
    }

    public async Task<PageResult<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, int page, int pageSize, string[] includes)
    {
        var query = GetQueryable(includes).Where(criteria);
        var total = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PageResult<TEntity>
        {
            TotalItems = total,
            CurrentPage = page,
            PageSize = pageSize,
            Content = data
        };
    }

    public TEntity Update(TEntity entity)
    {
        Attach(entity);
        _context.Set<TEntity>().Update(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    private IQueryable<TEntity> GetQueryable(string[] includes)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}