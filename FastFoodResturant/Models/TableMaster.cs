namespace FastFoodResturant.Models
{
	public class TableMaster
	{
		public long TableId { get; set; }
		public required string TableName { get; set; }
		public required string FloorType { get; set; }
		public required int Capacity { get; set; }
		public required bool IsOccupied { get; set; }
		public required bool IsOutSide { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
