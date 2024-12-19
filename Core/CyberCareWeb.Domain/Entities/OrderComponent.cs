namespace CyberCareWeb.Domain.Entities;

public class OrderComponent 
{
	public Guid Id { get; set; }
	public Guid ComponentId { get; set; }
	public Component Component { get; set; }
	public Guid OrderId { get; set; }
	public Order Order { get; set; }
}
