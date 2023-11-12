using FastFoodResturant.Helpers;
using FastFoodResturant.Models;

namespace FastFoodResturant.ViewModels.ListViewModels
{
    public class FoodMasterListViewModel  : CommonModel
    {
        public IQueryable<FoodMaster> FoodMasterList { get; set; }
    }
}
