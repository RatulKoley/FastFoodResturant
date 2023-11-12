using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextToppingsMaster
	{
		public static ModelBuilder AddToppingsMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<ToppingsMaster>(entity =>
			{
				entity.ToTable("toppings_master");
				entity.HasKey(e => new { e.ToppingsMasterId });


				entity.Property(e => e.ToppingsMasterId).HasColumnName("ToppingsMasterId");
				entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
				entity.Property(e => e.GSTId).HasColumnName("GSTId");
				entity.Property(e => e.FoodMasterId).HasColumnName("FoodMasterId");

				entity.HasIndex(e => new { e.ToppingsMasterId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.CategoryId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.GSTId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.FoodMasterId, e.IsAvailable }).IsUnique();


				entity.Property(e => e.ToppingsName).HasMaxLength(100).HasColumnName("ToppingsName");
				entity.Property(e => e.ToppingsDescription).HasMaxLength(400).HasColumnName("ToppingsDescription");
				entity.Property(e => e.ToppingsImage).HasColumnName("ToppingsImage");
				entity.Property(e => e.ToppingsPrice)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("ToppingsPrice");

				entity.Property(e => e.IsGlutenFree).HasColumnName("IsGlutenFree");
				entity.Property(e => e.IsSpicy).HasColumnName("IsSpicy");
				entity.Property(e => e.IsVegan).HasColumnName("IsVegan");
				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");


				entity.HasOne(e => e.Category).WithMany(e => e.ToppingsMaster)
			   .HasForeignKey(e => new { e.CategoryId })
			   .HasConstraintName("FK_ToppingsMaster_Category");

				entity.HasOne(e => e.GSTMaster).WithMany(e => e.ToppingsMaster)
				.HasForeignKey(e => new { e.GSTId })
				.HasConstraintName("FK_ToppingsMaster_GSTMaster");

				entity.HasOne(e => e.FoodMaster).WithMany(e => e.ToppingsMaster)
			   .HasForeignKey(e => new { e.FoodMasterId })
			   .HasConstraintName("FK_ToppingsMaster_FoodMaster");

			});
		}
	}
}
