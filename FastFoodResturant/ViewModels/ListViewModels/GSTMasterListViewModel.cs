using FastFoodResturant.Helpers;
using FastFoodResturant.Models;

namespace FastFoodResturant.ViewModels.ListViewModels
{
    public class GSTMasterListViewModel : CommonModel
    {
        public IQueryable<GSTMaster> GSTMasterList { get; set; }
    }
}
