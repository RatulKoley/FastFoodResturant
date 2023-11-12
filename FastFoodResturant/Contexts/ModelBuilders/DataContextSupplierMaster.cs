using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextSupplierMaster
	{
		public static ModelBuilder AddSupplierMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<SupplierMaster>(entity =>
			{
				entity.ToTable("supplier_master");
				entity.HasKey(e => new { e.SupplierId });


				entity.Property(e => e.SupplierId).HasColumnName("SupplierId");

				entity.HasIndex(e => new { e.SupplierId, e.IsAvailable }).IsUnique();

				entity.Property(e => e.SupplierName).HasMaxLength(100).HasColumnName("SupplierName");
				entity.Property(e => e.SupplierAddress).HasMaxLength(400).HasColumnName("SupplierAddress");
				entity.Property(e => e.SupplierEmail).HasMaxLength(100).HasColumnName("SupplierEmail");
				entity.Property(e => e.SupplierPhone).HasMaxLength(100).HasColumnName("SupplierPhone");

				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

			});
		}
	}
}
