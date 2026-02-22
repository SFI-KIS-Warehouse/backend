using AbobaWH.Domain;
using AbobaWH.Models;

namespace AbobaWH;

public static class MapExtensions
{
	extension(UserDTO)
	{
		public static UserDTO FromDomain(User user)
		{
			return new UserDTO(user.Id, user.Login, user.Role);
		}
	}

	extension(ContractDTO)
	{
		public static ContractDTO FromDomain(Contract contract)
		{
			var productInfo = contract.ProductInfo
				.Select(item => new ContractItemDTO(item.Product!, item.Count, item.Price))
				.ToList();

			var result = new ContractDTO(contract.Id, contract.Provider, contract.Status, productInfo, contract.IsHidden);

			return result;
		}
	}

	extension(DeliveryScheduleEntryDTO)
	{
		public static DeliveryScheduleEntryDTO FromDomain(DeliveryScheduleEntry entry, ReceiptOrderEntry? relatedReceipt)
		{
			var result = new DeliveryScheduleEntryDTO(
				entry.Id, entry.Date,
				entry.ContractId, entry.Product,
				entry.Count,
				relatedReceipt?.ReceiptOrderId 
			);

			return result;
		}
	}

	extension(ReceiptOrderDTO)
	{
		public static ReceiptOrderDTO FromDomain(ReceiptOrder order)
		{
			var provider = order.ProductInfo![0].ScheduledDelivery!.Contract.Provider;

			var productInfo = order.ProductInfo.Select(ReceiptOrderEntryDTO.FromDomain);

			var result = new ReceiptOrderDTO(order.Id, order.Time, provider, productInfo);

			return result;
		}
	}

	extension(ReceiptOrderEntryDTO)
	{
		public static ReceiptOrderEntryDTO FromDomain(ReceiptOrderEntry entry)
		{
			var result = new ReceiptOrderEntryDTO(
				entry.ScheduledDelivery!.Product,
				entry.Count,
				entry.ScheduledDelivery.ContractId
			);

			return result;
		}
	}
}
