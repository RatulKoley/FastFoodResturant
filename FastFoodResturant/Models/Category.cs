using System.ComponentModel.DataAnnotations;

namespace FastFoodResturant.Models
{
	public class Category
	{
		public Category()
		{
			FoodMaster = new HashSet<FoodMaster>();
			ToppingsMaster = new HashSet<ToppingsMaster>();
		}
		public long CategoryId { get;set;}
        [Required(ErrorMessage = "* Required")]
        public required string CategoryName { get;set;}
		public string? CategoryDescription { get; set; }
		public byte[]? CategoryImage { get; set; }

		public required bool IsAvailable { get; set; }
		public required DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public ICollection<FoodMaster>? FoodMaster { get;set;}
		public ICollection<ToppingsMaster>? ToppingsMaster { get;set;}
	}
}
