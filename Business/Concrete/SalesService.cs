using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Sales;
using Entity.Dtos.SalesDetails;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SalesService : ISaleService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesDetailsRepository _salesDetailsRepository;
        public SalesService(IUserRepository userRepository, ISalesRepository salesRepository, ISalesDetailsRepository salesDetailsRepository)
        {
            _userRepository = userRepository;
            _salesRepository = salesRepository;
            _salesDetailsRepository = salesDetailsRepository;
        }

        public async Task<ApiResponse<NoData>> AddSalesAsync(PostSales sales, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var salesDetails = await _salesDetailsRepository.GetAllAsync(p => p.IsActive == true);

            decimal totalAmount = 0;
            foreach (var saleDetail in salesDetails)
            {
                decimal amount = saleDetail.Price * (decimal)saleDetail.Quantity;
                totalAmount += amount;
            }

            var a = new Sales
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                TotalAmount = totalAmount,
            };

            await _salesRepository.InsertAsync(a);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteSalesAsync(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _salesRepository.GetByIdAsync(id);
           

            getById.IsActive = false;
            getById.DeletedDate = DateTime.Now;
            getById.DeletedBy = currentUserId;
            await _salesRepository.UpdateAsync(getById);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<GetSales>> GetSales(long salesId, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<GetSales>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getSales = await _salesRepository.GetAsync(p => p.IsActive == true && p.Id == salesId);
            var getListDetails =  await  _salesDetailsRepository.GetAllAsync(p=>p.IsActive==true && p.SalesId==salesId);
            var mappingDetails = getListDetails.Select(p => new GetSalesDetails
            {
                SalesId = salesId,
                Id = salesId,
                Price = p.Price,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
            }).ToList();
            var mapping = new GetSales
            {

                Date = DateTime.Now,
                Id = salesId,
                TotalAmount = getSales.TotalAmount,
                 GetSalesDetails=mappingDetails
            };
            return ApiResponse<GetSales>.Success(StatusCodes.Status200OK,mapping);
        }

        public async Task<ApiResponse<List<GetSales>>> GetSalesAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetSales>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }


            var getList = await _salesRepository.GetAllAsync(p=>p.IsActive==true);
            var list = new List<GetSales>();

            foreach (var item in getList)
            {
                var getListSalesDetails = await _salesDetailsRepository.GetAllAsync(p => p.IsActive == true && p.SalesId == item.Id);
                var mappedSalesDetails = getListSalesDetails.Select(sd => new GetSalesDetails
                {
                  
                    Id = sd.Id,
                    SalesId = sd.SalesId,
                    ProductId = sd.ProductId,
                    Quantity = sd.Quantity,
                    Price = sd.Price,
                   
                }).ToList();
                var add = new GetSales
                {
                    Date = DateTime.Now,
                    Id = item.Id,
                    TotalAmount = item.TotalAmount,
                    GetSalesDetails= mappedSalesDetails
                };
                list.Add(add);

            }

            return ApiResponse<List<GetSales>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateSalesAsync(UpdateSales update, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var updateSales = new Sales
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                TotalAmount = update.TotalAmount,
                Date = DateTime.Now,
                 IsActive=true,
            };
            await _salesRepository.UpdateAsync(updateSales);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
