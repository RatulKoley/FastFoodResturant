using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextFoodMaster
	{
		public static ModelBuilder AddFoodMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<FoodMaster>(entity =>
			{
				entity.ToTable("food_master");
				entity.HasKey(e => new { e.FoodMasterId });


				entity.Property(e => e.FoodMasterId).HasColumnName("FoodMasterId");

				entity.HasIndex(e => new { e.FoodMasterId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.CategoryId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.GSTId, e.IsAvailable }).IsUnique();


				entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
				entity.Property(e => e.GSTId).HasColumnName("GSTId");
				entity.Property(e => e.PreparationTime).HasColumnName("PreparationTime");
				entity.Property(e => e.FoodName).HasMaxLength(50).HasColumnName("FoodName");
				entity.Property(e => e.FoodDescription).HasMaxLength(400).HasColumnName("FoodDescription");
				entity.Property(e => e.FoodType).HasMaxLength(50).HasColumnName("FoodType");
				entity.Property(e => e.FoodImage).HasColumnName("FoodImage");
				entity.Property(e => e.FoodPrice)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("FoodPrice");
				entity.Property(e => e.Fat).HasColumnName("Fat");
				entity.Property(e => e.Calories).HasColumnName("Calories");
				entity.Property(e => e.Protein).HasColumnName("Protein");
				entity.Property(e => e.Carbohydrates).HasColumnName("Carbohydrates");
				entity.Property(e => e.Fiber).HasColumnName("Fiber");

				entity.Property(e => e.IsSpicy).HasColumnName("IsSpicy");
				entity.Property(e => e.IsVegan).HasColumnName("IsVegan");
				entity.Property(e => e.IsGlutenFree).HasColumnName("IsGlutenFree");
				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

				entity.HasOne(e => e.Category).WithMany(e => e.FoodMaster)
			   .HasForeignKey(e => new { e.CategoryId })
			   .HasConstraintName("FK_FoodMaster_Category");

				entity.HasOne(e => e.GSTMaster).WithMany(e => e.FoodMaster)
				.HasForeignKey(e => new { e.GSTId })
				.HasConstraintName("FK_FoodMaster_GSTMaster");

			});
		}
	}
}
