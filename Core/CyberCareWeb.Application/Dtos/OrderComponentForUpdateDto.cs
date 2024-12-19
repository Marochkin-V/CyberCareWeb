namespace CyberCareWeb.Application.Dtos;

public class OrderComponentForUpdateDto 
{
	public Guid Id { get; set; }
	public Guid ComponentId { get; set; }
	public Guid OrderId { get; set; }
}

