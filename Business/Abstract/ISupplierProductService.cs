using Entity.Dtos.Role;
using Entity.Dtos.Supplier;
using Entity.Dtos.SupplierProduct;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISupplierProductService
    {
        Task<ApiResponse<List<GetSupplierProduct>>> GetSupplierProductAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddSupplierProduct(PostSupplierProduct dto, int currentUserId);
        Task<ApiResponse<NoData>> DeleteSupplierProductAsync(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateSupplierProductAsync(UpdateSupplierProduct supplierProduct, int currentUserId);
    }
}
