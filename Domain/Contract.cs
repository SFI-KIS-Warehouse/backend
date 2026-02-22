namespace AbobaWH.Domain;

public class Contract
{
	public int Id { get; set; }

	public Provider Provider { get; set; }

	public int ProviderId { get; set; }

	public ContractStatuses Status { get; set; }

	public List<ContractItem> ProductInfo { get; set; }

	public bool IsHidden { get; set; }

	public Contract(Provider provider, List<ContractItem> productInfo)
	{
		Provider = provider;
		Status = ContractStatuses.Created;
		ProductInfo = productInfo;
	}

	protected Contract(int id, ContractStatuses status, bool isHidden)
	{
		Id = id;
		Provider = null!;
		Status = status;
		ProductInfo = null!;
		IsHidden = isHidden;
	}
}
