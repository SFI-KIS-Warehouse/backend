using AbobaWH.Domain;

namespace AbobaWH.Models;

public class ReceiptOrderDTO
{
	public int Id { get; set; }
	public DateTimeOffset Time { get; set; }

	public Provider Provider { get; set; }

	public List<ReceiptOrderEntryDTO> ProductInfo { get; set; }

	public ReceiptOrderDTO(int id, DateTimeOffset time, Provider provider, IEnumerable<ReceiptOrderEntryDTO> productInfo)
	{
		Id = id;
		Time = time;
		Provider = provider;
		ProductInfo = productInfo.ToList();
	}
}
