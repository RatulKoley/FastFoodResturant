using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextPurchaseMaster
	{
		public static ModelBuilder AddPurchaseProductMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<PurchaseProductMaster>(entity =>
			{
				entity.ToTable("purchase_product_master");
				entity.HasKey(e => new { e.ProductPurchaseMasterId });


				entity.Property(e => e.ProductPurchaseMasterId).HasColumnName("ProductPurchaseMasterId");
				entity.Property(e => e.SupplierId).HasColumnName("SupplierId");

				entity.HasIndex(e => new { e.ProductPurchaseMasterId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.SupplierId, e.IsAvailable }).IsUnique();


				entity.Property(e => e.NetAmount)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("NetAmount");
				entity.Property(e => e.TaxAmount)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("TaxAmount");
				entity.Property(e => e.DiscountAmount)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("DiscountAmount");
				entity.Property(e => e.PaidAmount)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("PaidAmount");
				entity.Property(e => e.AdjustAmount)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("AdjustAmount");

				entity.Property(e => e.IsPaid).HasColumnName("IsPaid");
				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

				entity.HasOne(e => e.SupplierMaster).WithMany(e => e.PurchaseProductMaster)
			   .HasForeignKey(e => new { e.SupplierId })
			   .HasConstraintName("FK_PurchaseProductMaster_SupplierMaster");

			});
		}
	}
}
