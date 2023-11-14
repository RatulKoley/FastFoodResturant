using FastFoodResturant.Helpers;
using FastFoodResturant.Models;

namespace FastFoodResturant.ViewModels.ListViewModels
{
    public class ProductMasterListViewModel  : CommonModel
    {
        public IQueryable<ProductMaster> ProductMasterList { get;set;}
    }
}
