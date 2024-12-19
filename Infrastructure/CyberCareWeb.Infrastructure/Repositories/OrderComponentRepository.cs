using Microsoft.EntityFrameworkCore;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;

namespace CyberCareWeb.Infrastructure.Repositories;

public class OrderComponentRepository(AppDbContext dbContext) : IOrderComponentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(OrderComponent entity) => await _dbContext.OrderComponents.AddAsync(entity);

    public async Task<IEnumerable<OrderComponent>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.OrderComponents.Include(e => e.Component).Include(e => e.Order).AsNoTracking() 
            : _dbContext.OrderComponents.Include(e => e.Component).Include(e => e.Order)).ToListAsync();

    public async Task<OrderComponent?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.OrderComponents.Include(e => e.Component).Include(e => e.Order).AsNoTracking() :
            _dbContext.OrderComponents.Include(e => e.Component).Include(e => e.Order)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(OrderComponent entity) => _dbContext.OrderComponents.Remove(entity);

    public void Update(OrderComponent entity) => _dbContext.OrderComponents.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

