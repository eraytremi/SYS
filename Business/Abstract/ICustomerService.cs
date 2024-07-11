using Entity.Dtos.Customer;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<ApiResponse<List<GetCustomer>>> GetCustomers(long currentUserId);
        Task<ApiResponse<NoData>> AddCustomer(AddCustomer customer, long currentUserId);
        Task<ApiResponse<NoData>> DeleteCustomer(int id, long currentUserId);
        Task<ApiResponse<NoData>> UpdateCustomer(UpdateCustomer customer, long currentUserId);
        Task<ApiResponse<GetCustomer>> GetCustomerById(long id, long currentUserId);

    }
}
