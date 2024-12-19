namespace CyberCareWeb.Domain.Entities;

public class Component 
{
	public Guid Id { get; set; }
	public Guid ComponentTypeId { get; set; }
	public ComponentType ComponentType { get; set; }
	public string Brand { get; set; } = "Not set yet";
    public string Manufactorer { get; set; } = "Not set yet";
    public string Specifications { get; set; } = "Not set yet";
    public int WarrantyPeriod { get; set; } = 0;
	public decimal Price { get; set; } = 0;
}
