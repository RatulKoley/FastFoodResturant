namespace FastFoodResturant.Models
{
	public class ProductMaster
	{
		public ProductMaster()
		{
			PurchaseProductDetails = new HashSet<PurchaseProductDetails>();
		}
		public long ProductId { get; set; }
		public required long UnitId { get; set; }
		public required long StockId { get; set; }
		public required long GSTId { get; set; }
		public required string ProductName { get; set; }
		public string? ProductDescription { get; set; }
		public byte[]? ProductImage { get; set; }
		public required double ProductPrice { get; set; }
		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public ProductStock? ProductStock { get;set;}
		public UnitMaster? UnitMaster { get;set;}
		public GSTMaster? GSTMaster { get;set;}
		public ICollection<PurchaseProductDetails>? PurchaseProductDetails { get;set;}
	}
}
