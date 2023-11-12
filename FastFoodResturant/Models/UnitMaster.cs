namespace FastFoodResturant.Models
{
	public class UnitMaster
	{
		public UnitMaster()
		{
			ProductMaster = new HashSet<ProductMaster>();
		}
		public long UnitId { get; set; }
		public required string UnitName { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public ICollection<ProductMaster>? ProductMaster { get; set; }
	}
}
