using CyberCareWeb.Domain.Entities;

namespace CyberCareWeb.Domain.Abstractions;

public interface IComponentRepository 
{
	Task<IEnumerable<Component>> Get(bool trackChanges);
	Task<Component?> GetById(Guid id, bool trackChanges);
    Task Create(Component entity);
    void Delete(Component entity);
    void Update(Component entity);
    Task SaveChanges();
}

