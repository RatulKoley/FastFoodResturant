using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextProductStock
	{
		public static ModelBuilder AddProductStock(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<ProductStock>(entity =>
			{
				entity.ToTable("product_stock");
				entity.HasKey(e => new { e.ProductStockId });


				entity.Property(e => e.ProductStockId).HasColumnName("ProductStockId");
				entity.Property(e => e.ProductId).HasColumnName("ProductId");

				entity.HasIndex(e => new { e.ProductStockId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.ProductId, e.IsAvailable }).IsUnique();

				entity.Property(e => e.StockQuantity)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("StockQuantity");

				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

				entity.HasOne(e => e.ProductMaster)
					.WithOne(e => e.ProductStock)
					.HasForeignKey<ProductStock>(e => e.ProductId)
					.HasConstraintName("FK_ProductStock_ProductMaster");

			});
		}
	}
}
