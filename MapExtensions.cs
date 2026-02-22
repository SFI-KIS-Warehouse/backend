using AbobaWH.Domain;
using AbobaWH.Models;

namespace AbobaWH;

public static class MapExtensions
{
	extension(ContractDTO)
	{
		public static ContractDTO FromDomain(Contract contract)
		{
			var productInfo = contract.ProductInfo
				.Select(item => new ProductCountPrice(item.ProductId, item.Count, item.Price))
				.ToList();

			var result = new ContractDTO(contract.Id, contract.Provider, contract.Status, productInfo);

			return result;
		}
	}
}
