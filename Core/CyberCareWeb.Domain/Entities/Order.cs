namespace CyberCareWeb.Domain.Entities;

public class Order 
{
	public Guid Id { get; set; }
	public DateTime OrderDate { get; set; } = DateTime.Now;
	public bool PaymentStatus { get; set; } = false;
	public bool CompletionStatus { get; set; } = false;
	public decimal TotalCost { get; set; } = 0;
	public int WarrantyPeriod { get; set; } = 0;
}
