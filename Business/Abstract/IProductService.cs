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
        Task<ApiResponse<List<GetProduct>>> GetProductAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddProductAsync(AddProduct product, int currentUserId);
        Task<ApiResponse<NoData>> DeleteProductAsync(long id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateProductAsync(UpdateProduct updateProduct, int currentUserId);
        Task<ApiResponse<GetProduct>> GetProductByBarcode(string barcode,int currentUserId);
    }
}
