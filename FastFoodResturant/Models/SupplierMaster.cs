using FastFoodResturant.Helpers;

namespace FastFoodResturant.Models
{
	public class SupplierMaster	
	{
		public SupplierMaster()
		{
			PurchaseProductMaster = new HashSet<PurchaseProductMaster>();
		}
		public long SupplierId { get; set; }
		public required string SupplierName { get; set; }
		public string? SupplierAddress { get; set; }
		public string? SupplierEmail { get; set; }
		public string? SupplierPhone { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public ICollection<PurchaseProductMaster>? PurchaseProductMaster { get; set; }
	}
}
