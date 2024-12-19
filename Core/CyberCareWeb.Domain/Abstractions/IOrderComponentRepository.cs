using CyberCareWeb.Domain.Entities;

namespace CyberCareWeb.Domain.Abstractions;

public interface IOrderComponentRepository 
{
	Task<IEnumerable<OrderComponent>> Get(bool trackChanges);
	Task<OrderComponent?> GetById(Guid id, bool trackChanges);
    Task Create(OrderComponent entity);
    void Delete(OrderComponent entity);
    void Update(OrderComponent entity);
    Task SaveChanges();
}

