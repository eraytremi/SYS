using Business.Abstract;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity;
using Entity.Dtos.UserClientRole;
using Entity.Dtos.UserRole;
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
    public class UserClientRoleService : IUserClientRoleService
    {       

        private readonly IUserClientRoleRepository _repo;
        private readonly IUserClientRepository _userClientRepository;
        public UserClientRoleService(IUserClientRoleRepository repo, IUserClientRepository userClientRepository)
        {
            _repo = repo;
            _userClientRepository = userClientRepository;
        }

        public async Task<ApiResponse<NoData>> AddUserClientRole(PostUserClientRole userRole, int currentUserId)
        {
            var getUser = await _userClientRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var addUserRole = new UserClientRole
            {
                RoleId = userRole.RoleId,
                UserClientId = currentUserId
            };

            await _repo.InsertAsync(addUserRole);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteUserClientRole(int id, int currentUserId)
        {
            var getUser = await _userClientRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var getUserRoleById = await _repo.GetByIdAsync(id);

            getUserRoleById.IsActive = false;
            getUserRoleById.DeletedDate = DateTime.Now;
            getUserRoleById.CreatedBy = currentUserId;
            await _repo.UpdateAsync(getUserRoleById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetUserClientRole>>> GetByIdAsync(int id, int currentUserId)
        {
            var getUser = await _userClientRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetUserClientRole>>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var getUserRoles = await _repo.GetAllAsync(p => p.UserClientId == currentUserId);
            var list = new List<GetUserClientRole>();
            foreach (var getUserRole in getUserRoles)
            {
                var userRole = new GetUserClientRole
                {
                    Id = id,
                    RoleId = getUserRole.RoleId,
                    UserClientId = getUserRole.UserClientId
                };
                list.Add(userRole);
            }

            return ApiResponse<List<GetUserClientRole>>.Success(StatusCodes.Status200OK, list);
        }

        public  async Task<ApiResponse<List<GetUserClientRole>>> GetUserClientRolesAsync(int currentUserId)
        {
            var getUser = await _userClientRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetUserClientRole>>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }
            var getUserRoleList = await _repo.GetAllAsync(p => p.IsActive == true);

            var userRoleList = new List<GetUserClientRole>();

            foreach (var userRole in getUserRoleList)
            {
                var addUserRole = new GetUserClientRole
                {
                    UserClientId = userRole.UserClientId,
                    RoleId = userRole.RoleId,
                    Id = userRole.Id
                };
                userRoleList.Add(addUserRole);
            }

            return ApiResponse<List<GetUserClientRole>>.Success(StatusCodes.Status200OK, userRoleList);
        }

        public async Task<ApiResponse<NoData>> UpdateUserClientRoleAsync(UpdateUserClientRole updateRole, int currentUserId)
        {
            var getUser = await _userClientRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var update = new UserClientRole
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                UserClientId = updateRole.UserClientId,
                RoleId = updateRole.RoleId,
                Id = updateRole.Id

            };

            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
