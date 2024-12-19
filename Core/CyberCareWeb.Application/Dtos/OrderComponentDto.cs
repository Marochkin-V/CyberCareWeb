namespace CyberCareWeb.Application.Dtos;

public class OrderComponentDto 
{
	public Guid Id { get; set; }
	public Guid ComponentId { get; set; }
	public ComponentDto Component { get; set; }
	public Guid OrderId { get; set; }
	public OrderDto Order { get; set; }
}

