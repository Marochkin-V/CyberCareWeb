using CyberCareWeb.Domain.Entities;

namespace CyberCareWeb.Domain.Abstractions;

public interface IOrderRepository 
{
	Task<IEnumerable<Order>> Get(bool trackChanges);
	Task<Order?> GetById(Guid id, bool trackChanges);
    Task Create(Order entity);
    void Delete(Order entity);
    void Update(Order entity);
    Task SaveChanges();
}

