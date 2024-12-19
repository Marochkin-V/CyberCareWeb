using CyberCareWeb.Domain.Entities;

namespace CyberCareWeb.Domain.Abstractions;

public interface IComponentTypeRepository 
{
	Task<IEnumerable<ComponentType>> Get(bool trackChanges);
	Task<ComponentType?> GetById(Guid id, bool trackChanges);
    Task Create(ComponentType entity);
    void Delete(ComponentType entity);
    void Update(ComponentType entity);
    Task SaveChanges();
}

