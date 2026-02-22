using AbobaWH.Domain;

namespace AbobaWH.Models;

public class ContractDTO
{
	public int Id { get; set; }

	public Provider Provider { get; set; }

	public ContractStatuses Status { get; set; }

	public List<ContractItemDTO> ProductInfo { get; set; }

	public bool IsHidden { get; set; }

	public ContractDTO(
		int id,
		Provider provider,
		ContractStatuses status, 
		List<ContractItemDTO> productInfo,
		bool isHidden
		)
	{
		Id = id;
		Provider = provider;
		Status = status;
		ProductInfo = productInfo;
		IsHidden = isHidden;
	}
}
