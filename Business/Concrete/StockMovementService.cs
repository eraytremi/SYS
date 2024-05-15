using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.StockMovement;
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
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _repository;
        private readonly IUserRepository _userRepository;
        public StockMovementService(IStockMovementRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddAsync(PostStockMovement postStockMovement, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            if (postStockMovement.IsEntry == true)
            {
                var add = new StockMovement
                {
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ProductId = postStockMovement.ProductId,
                    Quantity = postStockMovement.Quantity,
                    IsEntry = true,
                    SourceDestination = postStockMovement.SourceDestination,
                    Date = DateTime.Now
                };
                await _repository.InsertAsync(add);
                return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
            }

            var insert = new StockMovement
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = postStockMovement.ProductId,
                Date = DateTime.Now,
                IsEntry = false,
                SourceDestination = postStockMovement.SourceDestination,
                Quantity = postStockMovement.Quantity

            };
            await _repository.InsertAsync(insert);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById =  await _repository.GetByIdAsync(id);
            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            getById.IsActive = false;
            await _repository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<List<GetStockMovement>>> GetAllAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStockMovement>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            } 

            var getList = await _repository.GetAllAsync();
            var list = new List<GetStockMovement>();
            foreach (var item in getList)
            {
                var add = new GetStockMovement
                {
                    Date = DateTime.Now,
                    Id = item.Id,
                    IsEntry = item.IsEntry,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                list.Add(add);
            }
            return ApiResponse<List<GetStockMovement>>.Success(StatusCodes.Status200OK,list);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(UpdateStockMovement updateStockMovement, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new StockMovement
            {
                Date = DateTime.Now,
                Id = updateStockMovement.Id,
                IsEntry = updateStockMovement.IsEntry,
                ProductId = updateStockMovement.ProductId,
                Quantity = updateStockMovement.Quantity
            };
            await _repository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
