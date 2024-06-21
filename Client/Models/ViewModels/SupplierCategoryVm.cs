using Client.Models.Dtos.Category;
using Client.Models.Dtos.Supplier;

namespace Client.Models.ViewModels
{
    public class SupplierCategoryVm
    {
        public List<GetSupplier> Supplier { get; set; }
        public List<GetCategory> Category { get; set; }

    }
}
