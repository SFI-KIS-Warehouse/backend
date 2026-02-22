using AbobaWH.Domain;

namespace AbobaWH.Models;

public class ContractDTO
{
	public int Id { get; set; }

	public Provider Provider { get; set; }

	public ContractStatuses Status { get; set; }

	public List<ProductCountPrice> ProductInfo { get; set; }

	public ContractDTO(int id, Provider provider, ContractStatuses status, List<ProductCountPrice> productInfo)
	{
		Id = id;
		Provider = provider;
		Status = status;
		ProductInfo = productInfo;
	}
}
