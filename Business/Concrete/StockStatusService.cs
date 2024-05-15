using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.StockStatus;
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
    public class StockStatusService : IStockStatusService
    {
        private readonly IStockStatusRepository _stockStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public StockStatusService(IStockStatusRepository stockStatusRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _stockStatusRepository = stockStatusRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<NoData>> AddAsync(PostStockStatus postStockStatus, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new StockStatus
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = postStockStatus.ProductId,
                Quantity = postStockStatus.Quantity

            };

            await _stockStatusRepository.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);

        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _stockStatusRepository.GetByIdAsync(id);
            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            getById.IsActive = false;
            await _stockStatusRepository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetStockStatus>>> GetAllAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStockStatus>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _stockStatusRepository.GetAllAsync();
            var list = new List<GetStockStatus>();
            
            foreach (var item in getList)
            {
                var add = new GetStockStatus
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                list.Add(add);
            }

            return ApiResponse<List<GetStockStatus>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(UpdateStockStatus updateStockStatus, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new StockStatus
            {
                Id = updateStockStatus.Id,
                ProductId = updateStockStatus.ProductId,
                Quantity = updateStockStatus.Quantity,
            };

            await _stockStatusRepository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
