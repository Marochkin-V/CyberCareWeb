namespace CyberCareWeb.Application.Dtos;

public class OrderForUpdateDto 
{
	public Guid Id { get; set; }
	public DateTime OrderDate { get; set; }
	public bool PaymentStatus { get; set; }
	public bool CompletionStatus { get; set; }
	public decimal TotalCost { get; set; }
	public int WarrantyPeriod { get; set; }
}

