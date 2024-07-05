using Business.Abstract;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity.Dtos.SalesDetails;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SaleDetailsService:ISaleDetailsService
    {
        private readonly ISalesDetailsRepository _salesDetailsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISalesRepository _salesRepository;
        public SaleDetailsService(ISalesDetailsRepository salesDetailsRepository, IUserRepository userRepository, IProductRepository productRepository, ISalesRepository salesRepository)
        {
            _salesDetailsRepository = salesDetailsRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _salesRepository = salesRepository;
        }

        public async Task<ApiResponse<NoData>> AddSalesDetailsAsync(List<PostSalesDetails> sales, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            decimal totalAmount = 0;

            foreach (var item in sales)
            {
                decimal amount = item.Price * (decimal)item.Quantity;
                totalAmount += amount;
            }

            var addSales = new Sales
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                Date = DateTime.Now,
                TotalAmount = totalAmount
            };

            await _salesRepository.InsertAsync(addSales);

            var getSales =  await _salesRepository.GetAllAsync();
            var lastIndex = getSales.OrderByDescending(x => x.Id).FirstOrDefault();

            foreach (var sale in sales)
            {
                var product = await _salesDetailsRepository.GetByIdAsync(sale.ProductId);
                var add = new SalesDetails
                {
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    Price = product.Price,
                    Quantity = sale.Quantity,
                    ProductId = sale.ProductId,
                    SalesId = lastIndex.Id,
                    IsActive=true
                };

                await _salesDetailsRepository.InsertAsync(add);
               
            }
        
            
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteSalesDetailsAsync(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById =await _salesDetailsRepository.GetByIdAsync(id);

            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            getById.IsActive = false;
            await _salesDetailsRepository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetSalesDetails>>> GetSalesDetailsAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetSalesDetails>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _salesDetailsRepository.GetAllAsync(p=>p.IsActive==true);
            var list =  new List<GetSalesDetails>();

            foreach (var item in getList)
            {
                var add = new GetSalesDetails
                {
                    Id = item.Id,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SalesId=item.SalesId,
                };
                list.Add(add);  
            }
            return ApiResponse<List<GetSalesDetails>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateSalesDetailsAsync(UpdateSalesDetails update, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var updateDetails = new SalesDetails
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                ProductId = update.ProductId,
                SalesId = update.SalesId,
                Price = update.Price,
                Quantity = update.Quantity,
                IsActive = true,
                Id = update.Id

            };
             
            await _salesDetailsRepository.UpdateAsync(updateDetails);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
