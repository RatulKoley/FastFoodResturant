namespace FastFoodResturant.Models
{
	public class ToppingsMaster
	{
		public long ToppingsMasterId { get; set; }
		public required long CategoryId { get; set; }
		public required long GSTId { get; set; }
		public required long FoodMasterId { get; set; }
		public required string ToppingsName { get; set; }
		public string? ToppingsDescription { get; set; }
		public byte[]? ToppingsImage { get; set; }
		public required double ToppingsPrice { get; set; }

		public required bool IsSpicy { get; set; }
		public required bool IsVegan { get; set; }
		public required bool IsGlutenFree { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Category? Category { get; set; }
		public GSTMaster? GSTMaster { get; set; }
		public FoodMaster? FoodMaster { get; set; }
	}
}
