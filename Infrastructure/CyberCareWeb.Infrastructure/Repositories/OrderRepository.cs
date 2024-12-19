using Microsoft.EntityFrameworkCore;
using CyberCareWeb.Domain.Entities;
using CyberCareWeb.Domain.Abstractions;

namespace CyberCareWeb.Infrastructure.Repositories;

public class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Order entity) => await _dbContext.Orders.AddAsync(entity);

    public async Task<IEnumerable<Order>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Orders.AsNoTracking() 
            : _dbContext.Orders).ToListAsync();

    public async Task<Order?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Orders.AsNoTracking() :
            _dbContext.Orders).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Order entity) => _dbContext.Orders.Remove(entity);

    public void Update(Order entity) => _dbContext.Orders.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

    public async Task<int> CountAsync()
    {
        var orders = await _dbContext.Orders.ToListAsync();
        return orders.Count();
    }

    public async Task<IEnumerable<Order>> GetPageAsync(int page, int pageSize)
    {
        var orders = await _dbContext.Orders.OrderBy(d => d.Id).ToListAsync();
        return orders.Skip((page - 1) * pageSize).Take(pageSize);
    }
}

