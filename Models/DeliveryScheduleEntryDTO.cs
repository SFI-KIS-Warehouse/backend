using AbobaWH.Domain;

namespace AbobaWH.Models;

public class DeliveryScheduleEntryDTO
{
	public int Id { get; set; }
	public DateOnly Date { get; set; }
	public int Contract { get; set; }

	public Product Product { get; set; }

	public int Count { get; set; }

	public int? RelatedReceipt { get; set; }

	public DeliveryScheduleEntryDTO(int id, DateOnly date, int contract, Product product, int count, int? relatedReceipt)
	{
		Id = id;
		Date = date;
		Contract = contract;
		Product = product;
		Count = count;
		RelatedReceipt = relatedReceipt;
	}
}
