namespace FastFoodResturant.Models
{
	public class ProductStock
	{
		public long ProductStockId { get; set; }
		public required long ProductId { get; set; }
		public required double StockQuantity { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public ProductMaster? ProductMaster { get;set;}
	}
}
