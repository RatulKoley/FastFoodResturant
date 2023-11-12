namespace FastFoodResturant.Models
{
	public class GSTMaster
	{
		public GSTMaster()
		{
			FoodMaster = new HashSet<FoodMaster>();
			ToppingsMaster = new HashSet<ToppingsMaster>();
			ProductMaster = new HashSet<ProductMaster>();
		}
		public long GSTId { get; set; }
		public required string GSTName { get; set; }
		public double? GSTPercentage { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public ICollection<FoodMaster>? FoodMaster { get; set; }
		public ICollection<ToppingsMaster>? ToppingsMaster { get; set; }
		public ICollection<ProductMaster>? ProductMaster { get; set; }
	}
}
