namespace FastFoodResturant.Models
{
	public class FoodMaster
	{
		public FoodMaster()
		{
			ToppingsMaster = new HashSet<ToppingsMaster>();
		}
		public long FoodMasterId { get;set;}
		public required long CategoryId { get; set; }
		public required long GSTId { get; set; }
		public required string FoodName { get;set;}
		public required string FoodType { get;set;}
		public string? FoodDescription {get;set;}
		public byte[]? FoodImage {get;set;}
		public required double FoodPrice {get;set;}
		public required string PreparationTime { get; set; }

		public required bool IsSpicy { get; set; }
		public required bool IsVegan { get; set; }
		public required bool IsGlutenFree { get; set; }
		public double? Protein { get; set; }
		public double? Carbohydrates { get; set; }
		public double? Fat { get; set; }
		public double? Fiber { get; set; }
		public double? Calories { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Category? Category { get;set;}
		public GSTMaster? GSTMaster { get;set;}
		public ICollection<ToppingsMaster>? ToppingsMaster { get;set;}

	}
}
