using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextPurchaseDetails
	{
		public static ModelBuilder AddPurchaseProductDetails(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<PurchaseProductDetails>(entity =>
			{
				entity.ToTable("purchase_product_details");
				entity.HasKey(e => new { e.ProductPurchaseDetailsId });


				entity.Property(e => e.ProductPurchaseMasterId).HasColumnName("ProductPurchaseMasterId");
				entity.Property(e => e.ProductPurchaseDetailsId).HasColumnName("ProductPurchaseDetailsId");
				entity.Property(e => e.ItemId).HasColumnName("ItemId");

				entity.HasIndex(e => new { e.ProductPurchaseMasterId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.ProductPurchaseDetailsId, e.IsAvailable }).IsUnique();
				entity.HasIndex(e => new { e.ItemId, e.IsAvailable }).IsUnique();


				entity.Property(e => e.ItemQuantity)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("ItemQuantity");
				entity.Property(e => e.PerItemTradeValue)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("PerItemTradeValue");
				entity.Property(e => e.PerItemMRP)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("PerItemMRP");
				entity.Property(e => e.NetAmount)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("NetAmount");
				entity.Property(e => e.TaxAmount)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("TaxAmount");

				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

				entity.HasOne(e => e.ProductMaster).WithMany(e => e.PurchaseProductDetails)
			   .HasForeignKey(e => new { e.ItemId })
			   .HasConstraintName("FK_PurchaseProductDetails_ProductMaster");

				entity.HasOne(e => e.PurchaseProductMaster).WithMany(e => e.PurchaseProductDetails)
			   .HasForeignKey(e => new { e.ProductPurchaseMasterId })
			   .HasConstraintName("FK_PurchaseProductDetails_PurchaseProductMaster");

			});
		}
	}
}
