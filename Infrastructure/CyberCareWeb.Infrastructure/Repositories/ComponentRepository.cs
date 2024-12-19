using Microsoft.EntityFrameworkCore;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;

namespace CyberCareWeb.Infrastructure.Repositories;

public class ComponentRepository(AppDbContext dbContext) : IComponentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Component entity) => await _dbContext.Components.AddAsync(entity);

    public async Task<IEnumerable<Component>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Components.Include(e => e.ComponentType).AsNoTracking() 
            : _dbContext.Components.Include(e => e.ComponentType)).ToListAsync();

    public async Task<Component?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Components.Include(e => e.ComponentType).AsNoTracking() :
            _dbContext.Components.Include(e => e.ComponentType)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Component entity) => _dbContext.Components.Remove(entity);

    public void Update(Component entity) => _dbContext.Components.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

