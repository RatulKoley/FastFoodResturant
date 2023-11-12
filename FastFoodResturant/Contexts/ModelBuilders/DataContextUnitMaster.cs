using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextUnitMaster
	{
		public static ModelBuilder AddUnitMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<UnitMaster>(entity =>
			{
				entity.ToTable("unit_master");
				entity.HasKey(e => new { e.UnitId });


				entity.Property(e => e.UnitId).HasColumnName("UnitId");

				entity.HasIndex(e => new { e.UnitId, e.IsAvailable }).IsUnique();

				entity.Property(e => e.UnitName).HasMaxLength(100).HasColumnName("UnitName");

				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

			});
		}
	}
}
