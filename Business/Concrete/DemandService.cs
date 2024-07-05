using Business.Abstract;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity;
using Entity.Dtos.Demand;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DemandService : IDemandService
    {
        private readonly IDemandRepository _demandRepository;
        private readonly IUserRepository _userRepository;
        public DemandService(IDemandRepository demandRepository, IUserRepository userRepository)
        {
            _demandRepository = demandRepository;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddDemandAsync(PostDemand demand, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

           
            var add = new Demand
            {
                Description = demand.Description,
                ProductName = demand.ProductName,
                StatusType = StatusType.Bekleyen,
                CreatedBy= currentUserId,
                CreatedDate = DateTime.Now,
                Quantity = demand.Quantity,
                IsActive=true,         
            };

            
            await _demandRepository.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<List<GetDemand>>> ApprovedDemands(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetDemand>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _demandRepository.GetAllAsync(p=>p.IsActive==true && p.StatusType==StatusType.Onaylanan);
            var list =  new List<GetDemand>();

            foreach (var item in getList)
            {
                var add = new GetDemand
                {
                    Id = item.Id,
                    Description= item.Description,
                    ProductName = item.ProductName,
                    Quantity= item.Quantity,
                    UpdatedDate = item.UpdatedDate
                    
                };
                list.Add(add);
            }
            return ApiResponse<List<GetDemand>>.Success(StatusCodes.Status200OK, list);
        }


        public async Task<ApiResponse<NoData>> ApproveDemand(int id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getDemand = await _demandRepository.GetByIdAsync(id);
            getDemand.StatusType = StatusType.Onaylanan;
            getDemand.IsActive = true;
            getDemand.UpdatedDate = DateTime.Now;
            getDemand.UpdatedBy = currentUserId;
            
            await _demandRepository.UpdateAsync(getDemand);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> DeleteDemandAsync(int id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getDemand = await _demandRepository.GetByIdAsync(id);
            getDemand.DeletedBy = currentUserId;
            getDemand.DeletedDate = DateTime.Now;
            getDemand.IsActive = false;

            await _demandRepository.UpdateAsync(getDemand);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetDemand>>> GetDemandAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetDemand>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _demandRepository.GetAllAsync();
            var list = new List<GetDemand>();

            foreach (var item in getList)
            {
                var add = new Demand
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    StatusType = item.StatusType

                };
                await _demandRepository.InsertAsync(add);
            }
            return ApiResponse<List<GetDemand>>.Success(StatusCodes.Status200OK, list);

        }

        public async Task<ApiResponse<NoData>> RejectDemand(int id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _demandRepository.GetByIdAsync(id);
            getById.IsActive = true;
            getById.StatusType = StatusType.Reddedilmiş;
            getById.UpdatedBy=currentUserId;
            getById.UpdatedDate = DateTime.Now;
            await _demandRepository.UpdateAsync(getById);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetDemand>>> RejectedDemands(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetDemand>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _demandRepository.GetAllAsync(p=>p.StatusType==StatusType.Reddedilmiş && p.IsActive==true);
            var list = new List<GetDemand>();
            foreach (var item in getList)
            {
                var add = new GetDemand
                {
                    Description = item.Description,
                    Id = item.Id,
                    ProductName = item.ProductName,
                    UpdatedDate = DateTime.Now,
                    Quantity = item.Quantity,
                };
                list.Add(add);
            }

            return ApiResponse<List<GetDemand>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateDemandAsync(UpdateDemand demand, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new Demand
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                Id = demand.Id,
                Description = demand.Description,
                ProductName = demand.ProductName,
                Quantity = demand.Quantity

            };
            await _demandRepository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetDemand>>> WaitingDemands(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetDemand>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _demandRepository.GetAllAsync(p => p.StatusType == StatusType.Bekleyen && p.IsActive == true);
            
            var list = new List<GetDemand>();
            foreach (var item in getList)
            {
                var user =await _userRepository.GetByIdAsync(item.CreatedBy);
                var add = new GetDemand
                {
                    Description = item.Description,
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    CreatedDate=item.CreatedDate,
                    UpdatedDate=item.UpdatedDate,
                    CreatedBy=user.Name
                };
                list.Add(add);
            }

            return ApiResponse<List<GetDemand>>.Success(StatusCodes.Status200OK, list);
        }
    }
}
