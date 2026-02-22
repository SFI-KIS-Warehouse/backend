namespace AbobaWH.Domain;

public class Contract
{
	public int Id { get; set; }

	public Provider Provider { get; set; }

	public ContractStatuses Status { get; set; }

	public List<ContractItem> ProductInfo { get; set; }

	public Contract(Provider provider, List<ContractItem> productInfo)
	{
		Provider = provider;
		Status = ContractStatuses.Created;
		ProductInfo = productInfo;
	}

	protected Contract(int id, ContractStatuses status)
	{
		Id = id;
		Provider = null!;
		Status = status;
		ProductInfo = null!;
	}
}
