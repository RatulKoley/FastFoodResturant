using FastFoodResturant.Helpers;
using FastFoodResturant.Models;

namespace FastFoodResturant.ViewModels.ListViewModels
{
    public class UnitMasterListViewModel : CommonModel
    {
        public IQueryable<UnitMaster> UnitMasterList { get; set; }
    }
}
