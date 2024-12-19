using Microsoft.EntityFrameworkCore;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;
using System.Reflection;

namespace CyberCareWeb.Infrastructure.Repositories;

public class ComponentTypeRepository(AppDbContext dbContext) : IComponentTypeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(ComponentType entity) => await _dbContext.ComponentTypes.AddAsync(entity);

    public async Task<IEnumerable<ComponentType>> Get(bool trackChanges) =>
        await (!trackChanges
            ? _dbContext.ComponentTypes.AsNoTracking()
            : _dbContext.ComponentTypes).ToListAsync();

    public async Task<ComponentType?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.ComponentTypes.AsNoTracking() :
            _dbContext.ComponentTypes).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(ComponentType entity) => _dbContext.ComponentTypes.Remove(entity);

    public void Update(ComponentType entity) => _dbContext.ComponentTypes.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

    public async Task<int> CountAsync()
    {
        var componentTypes = await _dbContext.ComponentTypes.ToListAsync();
        return componentTypes.Count();
    }

    public async Task<IEnumerable<ComponentType>> GetPageAsync(int page, int pageSize)
    {
        var componentTypes = await _dbContext.ComponentTypes.OrderBy(d => d.Id).ToListAsync();
        return componentTypes.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
