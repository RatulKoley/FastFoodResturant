using FastFoodResturant.Helpers;
using FastFoodResturant.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FastFoodResturant.ViewModels.ListViewModels
{
	public class CategoryListViewModel	: CommonModel
	{
		public IQueryable<Category> CategoryList { get;set;}
	}
}
