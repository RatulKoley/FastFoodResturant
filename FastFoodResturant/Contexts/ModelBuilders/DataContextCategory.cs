using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodResturant.Contexts.ModelBuilders
{
	public static class DataContextCategory
	{
		public static ModelBuilder AddCategory(ModelBuilder modelBuilder)
		{
			return modelBuilder.Entity<Category>(entity =>
			{
				entity.ToTable("category");
				entity.HasKey(e => new { e.CategoryId });


				entity.Property(e => e.CategoryId).HasColumnName("CategoryId");

				entity.HasIndex(e => new { e.CategoryId, e.IsAvailable }).IsUnique();


				entity.Property(e => e.CategoryDescription).HasMaxLength(200).HasColumnName("CategoryDescription");
				entity.Property(e => e.CategoryImage).HasColumnName("CategoryImage");
				entity.Property(e => e.CategoryName).HasMaxLength(50).HasColumnName("CategoryName");

				entity.Property(e => e.IsAvailable).HasColumnName("IsAvailable");
				entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("CreatedAt");
				entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("UpdatedAt");

			});
		}
	}
}
