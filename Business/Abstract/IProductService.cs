using Entity.Dtos.Product;
using Entity.Dtos.Role;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<ApiResponse<List<GetProduct>>> GetProductAsync(long currentUserId, int pageNumber, int pageSize);
        Task<ApiResponse<NoData>> AddProductAsync(AddProduct product, long currentUserId);
        Task<ApiResponse<NoData>> DeleteProductAsync(long id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateProductAsync(UpdateProduct updateProduct, long currentUserId);
        Task<ApiResponse<GetProduct>> GetProductByBarcode(string barcode,long currentUserId);
        Task<ApiResponse<GetProduct>> GetProductByName(string name, long currentUserId);
    }
}
