using Business.Abstract;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity.Dtos.WareHouse;
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
    public class WareHouseService : IWareHouseService
    {
        private readonly IWareHouseRepository  _wareHouseRepository;
        private readonly IUserRepository _userRepository;

        public WareHouseService(IWareHouseRepository wareHouseRepository, IUserRepository userRepository)
        {
            _wareHouseRepository = wareHouseRepository;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddWareHouse(PostWareHouse postWareHouse, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new WareHouse
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                Name = postWareHouse.Name
            };
            await _wareHouseRepository.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteWareHouse(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _wareHouseRepository.GetByIdAsync(id);
            getById.IsActive = false;
            getById.DeletedDate= DateTime.Now;
            getById.DeletedBy= currentUserId;
            await _wareHouseRepository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetWareHouse>>> GetListWareHouseAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetWareHouse>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList =  await _wareHouseRepository.GetAllAsync();
            var list =  new List<GetWareHouse>();

            foreach (var item in getList)
            {
                var add = new GetWareHouse
                {
                    Id = item.Id,
                    Name = item.Name
                };
                list.Add(add);
            }
            return ApiResponse<List<GetWareHouse>>.Success(StatusCodes.Status200OK, list);

        }

        public async Task<ApiResponse<NoData>> UpdateWareHouseAsync(UpdateWareHouse updateWareHouse, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new WareHouse
            {
                Id = updateWareHouse.Id,
                Name = updateWareHouse.Name,
                DeletedBy = currentUserId,
                DeletedDate = DateTime.Now,
                IsActive = false
            };

            await _wareHouseRepository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
                
         }
    }
}
