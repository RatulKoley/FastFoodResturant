namespace FastFoodResturant.Models
{
	public class PurchaseProductDetails
	{
		public long ProductPurchaseDetailsId { get; set; }
		public required long ProductPurchaseMasterId { get; set; }
		public required long ItemId { get; set; }
		public required double ItemQuantity { get; set; }
		public required double PerItemMRP { get; set; }
		public required double PerItemTradeValue { get; set; }
		public required double NetAmount { get; set; }
		public required double TaxAmount { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public ProductMaster? ProductMaster { get; set; }
		public PurchaseProductMaster? PurchaseProductMaster { get; set; }
	}
}
