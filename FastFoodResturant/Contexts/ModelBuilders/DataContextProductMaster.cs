using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextProductMaster
	{
		public static ModelBuilder AddProductMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<ProductMaster>(entity =>
			{
				entity.ToTable("product_master");
				entity.HasKey(e => new { e.ProductId });


				entity.Property(e => e.ProductId).HasColumnName("ProductId");
				entity.Property(e => e.UnitId).HasColumnName("UnitId");
				entity.Property(e => e.StockId).HasColumnName("StockId");
				entity.Property(e => e.GSTId).HasColumnName("GSTId");

				entity.HasIndex(e => new { e.ProductId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.UnitId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.StockId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.GSTId, e.IsAvailable }).IsUnique();


				entity.Property(e => e.ProductName).HasMaxLength(100).HasColumnName("ProductName");
				entity.Property(e => e.ProductDescription).HasMaxLength(400).HasColumnName("ProductDescription");
				entity.Property(e => e.ProductImage).HasColumnName("ProductImage");

				entity.Property(e => e.ProductPrice)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("ProductPrice");

				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

				entity.HasOne(e => e.ProductStock)
					.WithOne(e => e.ProductMaster)
					.HasForeignKey<ProductMaster>(e => e.StockId) 
					.HasConstraintName("FK_ProductMaster_ProductStock");

				entity.HasOne(e => e.UnitMaster).WithMany(e => e.ProductMaster)
				.HasForeignKey(e => new { e.UnitId })
				.HasConstraintName("FK_ProductMaster_UnitMaster");

				entity.HasOne(e => e.GSTMaster).WithMany(e => e.ProductMaster)
				.HasForeignKey(e => new { e.GSTId })
				.HasConstraintName("FK_ProductMaster_GSTMaster");

			});
		}
	}
}
