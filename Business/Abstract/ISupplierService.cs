
using Entity.Dtos.Supplier;
using Infrastructure.Utilities.Responses;

namespace Business.Abstract
{
    public interface ISupplierService
    {
        Task<ApiResponse<List<GetSupplier>>> GetSupplierAsync( int currentUserId);
        Task<ApiResponse<NoData>> AddSupplierAsync( AddSupplier supplier, int currentUserId);
        Task<ApiResponse<NoData>> DeleteSupplierAsync(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateSupplierAsync(UpdateSupplier supplier, int currentUserId);
    }
}
