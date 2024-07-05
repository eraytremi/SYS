
using Entity.Dtos.Supplier;
using Infrastructure.Utilities.Responses;

namespace Business.Abstract
{
    public interface ISupplierService
    {
        Task<ApiResponse<List<GetSupplier>>> GetSupplierAsync( long currentUserId);
        Task<ApiResponse<NoData>> AddSupplierAsync( AddSupplier supplier, long currentUserId);
        Task<ApiResponse<NoData>> DeleteSupplierAsync(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateSupplierAsync(UpdateSupplier supplier, long currentUserId);
    }
}
