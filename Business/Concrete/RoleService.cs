using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity;
using Entity.Dtos.Role;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        private readonly IUserRepository _userRepository;
        public RoleService(IRoleRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddRole(AddRole role, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var mappingRole = new Role
            {
                CreatedDate = DateTime.Now,
                CreatedBy = currentUserId,
                Name = role.Name,
                IsActive=true
            };

            await _repo.InsertAsync(mappingRole);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteRole(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var getRoleById = await _repo.GetByIdAsync(id);
            getRoleById.IsActive = false;
            getRoleById.DeletedDate = DateTime.Now;
            await _repo.UpdateAsync(getRoleById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<List<GetRole>>> GetRolesAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetRole>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var getRolesList= await _repo.GetAllAsync(p => p.IsActive == true);
            var roleList = new List<GetRole>();
            foreach (var getRole in getRolesList)
            {
                var role = new GetRole
                {
                    Id = getRole.Id,
                    Name = getRole.Name
                };
                roleList.Add(role);
            }

            return ApiResponse<List<GetRole>>.Success(StatusCodes.Status200OK, roleList);
        }

        public async Task<ApiResponse<NoData>> UpdateRoleAsync(UpdateRole updateRole, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var update = new Role
            {
                Id = updateRole.Id,
                UpdatedDate = DateTime.Now,
                UpdatedBy = currentUserId,
                Name = updateRole.Name

            };
            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
