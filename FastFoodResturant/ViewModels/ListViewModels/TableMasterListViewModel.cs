using FastFoodResturant.Helpers;
using FastFoodResturant.Models;

namespace FastFoodResturant.ViewModels.ListViewModels
{
    public class TableMasterListViewModel : CommonModel
    {
        public IQueryable<TableMaster> TableMasterList { get; set; }
    }
}
