using FastFoodResturant.Helpers;
using FastFoodResturant.Models;

namespace FastFoodResturant.ViewModels.ListViewModels
{
    public class SupplierListViewModel  : CommonModel
    {
        public IQueryable<SupplierMaster> SupplierMasterList { get;set;}
    }
}
