using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextTableMaster
	{
		public static ModelBuilder AddTableMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<TableMaster>(entity =>
			{
				entity.ToTable("table_master");
				entity.HasKey(e => new { e.TableId });


				entity.Property(e => e.TableId).HasColumnName("TableId");
				entity.Property(e => e.Capacity).HasColumnName("Capacity");
				entity.Property(e => e.IsOccupied).HasColumnName("IsOccupied");

				entity.HasIndex(e => new { e.TableId, e.IsAvailable }).IsUnique();

				entity.Property(e => e.TableName).HasMaxLength(100).HasColumnName("TableName");
				entity.Property(e => e.FloorType).HasMaxLength(100).HasColumnName("FloorType");

				entity.Property(e => e.IsOutSide).HasColumnName("IsOutSide");
				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

			});
		}
	}
}
