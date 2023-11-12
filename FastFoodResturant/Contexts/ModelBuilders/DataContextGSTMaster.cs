using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextGSTMaster
	{
		public static ModelBuilder AddGSTMaster(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<GSTMaster>(entity =>
			{
				entity.ToTable("gst_master");
				entity.HasKey(e => new { e.GSTId });


				entity.Property(e => e.GSTId).HasColumnName("GSTId");

				entity.HasIndex(e => new { e.GSTId, e.IsAvailable }).IsUnique();


				entity.Property(e => e.GSTName).HasMaxLength(100).HasColumnName("GSTName");
				entity.Property(e => e.GSTPercentage)
				   .HasColumnType("decimal(18, 2)")
				   .HasColumnName("GSTPercentage");

				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

			});
		}
	}
}
