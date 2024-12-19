namespace CyberCareWeb.Application.Dtos;

public class ComponentDto 
{
	public Guid Id { get; set; }
	public Guid ComponentTypeId { get; set; }
	public ComponentTypeDto ComponentType { get; set; }
	public string Brand { get; set; }
	public string Manufactorer { get; set; }
	public string Specifications { get; set; }
	public int WarrantyPeriod { get; set; }
	public decimal Price { get; set; }
}

