namespace CyberCareWeb.Application.Dtos;

public class OrderForCreationDto 
{
	public DateTime OrderDate { get; set; }
	public bool PaymentStatus { get; set; }
	public bool CompletionStatus { get; set; }
	public decimal TotalCost { get; set; }
	public int WarrantyPeriod { get; set; }
}

