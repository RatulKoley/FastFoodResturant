namespace FastFoodResturant.Models
{
	public class PurchaseProductMaster
	{
		public PurchaseProductMaster()
		{
			PurchaseProductDetails = new HashSet<PurchaseProductDetails>();
		}
		public long ProductPurchaseMasterId { get; set; }
		public required long SupplierId { get; set; }
		public required double NetAmount { get; set; }
		public required double TaxAmount { get; set; }
		public required double DiscountAmount { get; set; }
		public required double PaidAmount { get; set; }
		public required double AdjustAmount { get; set; }
		public required bool IsPaid { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public SupplierMaster? SupplierMaster { get;set;}
		public ICollection<PurchaseProductDetails>? PurchaseProductDetails { get; set; }
	}
}
