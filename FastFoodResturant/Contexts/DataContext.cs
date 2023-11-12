using FastFoodResturant.Contexts.ModelBuilders;
using FastFoodResturant.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FastFoodResturant.Contexts
{
	public class DataContext  : DbContext
	{
		public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

		public virtual DbSet<Category> Category { get;set;}
		public virtual DbSet<FoodMaster> FoodMaster { get;set;}
		public virtual DbSet<GSTMaster> GSTMaster { get;set;}
		public virtual DbSet<ProductMaster> ProductMaster { get;set;}
		public virtual DbSet<ProductStock> ProductStock { get;set;}
		public virtual DbSet<PurchaseProductMaster> PurchaseProductMaster { get;set;}
		public virtual DbSet<PurchaseProductDetails> PurchaseProductDetails { get;set;}
		public virtual DbSet<SupplierMaster> SupplierMaster { get;set;}
		public virtual DbSet<TableMaster> TableMaster { get;set;}
		public virtual DbSet<ToppingsMaster> ToppingsMaster { get;set;}
		public virtual DbSet<UnitMaster> UnitMaster { get;set;}

		protected override void OnModelCreating(ModelBuilder modelbuilder)
		{ 
			DataContextCategory.AddCategory(modelbuilder);
			DataContextFoodMaster.AddFoodMaster(modelbuilder);
			DataContextGSTMaster.AddGSTMaster(modelbuilder);
			DataContextProductMaster.AddProductMaster(modelbuilder);
			DataContextProductStock.AddProductStock(modelbuilder);
			DataContextPurchaseDetails.AddPurchaseProductDetails(modelbuilder);
			DataContextPurchaseMaster.AddPurchaseProductMaster(modelbuilder);
			DataContextSupplierMaster.AddSupplierMaster(modelbuilder);
			DataContextTableMaster.AddTableMaster(modelbuilder);
			DataContextToppingsMaster.AddToppingsMaster(modelbuilder);
			DataContextUnitMaster.AddUnitMaster(modelbuilder);
		}
	}
}
