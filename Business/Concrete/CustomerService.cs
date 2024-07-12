using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Customer;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IUserRepository _userRepository;
        public CustomerService(ICustomerRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<Customer>> AddCustomer(AddCustomer customer, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<Customer>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var existCustomer = await _repo.GetAsync(p => p.Name == customer.Name && p.Mail==customer.Mail && p.IsActive==true);

            if (existCustomer != null)
            {
                return ApiResponse<Customer>.Success(StatusCodes.Status200OK, existCustomer);
            }

            var add = new Customer
            {
                Address = customer.Address,
                CreatedBy = currentUserId,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                Name = customer.Name,
                Mail = customer.Mail,
                PhoneNumber = customer.PhoneNumber,
                CompanyName = customer.CompanyName     
            };

            await _repo.InsertAsync(add);
            return ApiResponse<Customer>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteCustomer(int id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _repo.GetByIdAsync(id); 
            getById.IsActive = false;
            getById.DeletedDate = DateTime.UtcNow;
            getById.DeletedBy = currentUserId;
            await _repo.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<GetCustomer>> GetCustomerById(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<GetCustomer>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getCustomer = await _repo.GetByIdAsync(id);

            var customer = new GetCustomer
            {
                Address = getCustomer.Address,
                CompanyName = getCustomer.CompanyName,
                Id = getCustomer.Id,
                Mail = getCustomer.Mail,
                Name = getCustomer.Name,
                PhoneNumber = getCustomer.PhoneNumber
            };

            return ApiResponse<GetCustomer>.Success(StatusCodes.Status200OK, customer);
        }

        public async Task<ApiResponse<List<GetCustomer>>> GetCustomers(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetCustomer>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _repo.GetAllAsync(p=>p.IsActive==true);
            var list = new List<GetCustomer>();

            foreach (var item in getList)
            {
                var add = new GetCustomer
                {
                    Address = item.Address,
                    Id = item.Id,
                    Name = item.Name,
                    Mail = item.Mail,
                    PhoneNumber = item.PhoneNumber
                };
                list.Add(add);
            }
            return ApiResponse<List<GetCustomer>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateCustomer(UpdateCustomer customer, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new Customer
            {
                Address = customer.Address,
                IsActive = true,
                Name = customer.Name,
                Mail = customer.Mail,
                PhoneNumber = customer.PhoneNumber,
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                Id = customer.Id

            };
            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
