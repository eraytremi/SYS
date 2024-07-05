using Entity.Dtos.Category;
using Entity.Dtos.Role;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<GetCategory>>> GetCategoriesAsync(long currentUserId);
        Task<ApiResponse<NoData>> AddCategory(AddCategory category, long currentUserId);
        Task<ApiResponse<NoData>> DeleteCategory(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateCategoryAsync(UpdateCategory updateCategory, long currentUserId);
    }
}
