using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity;
using Entity.Dtos.UserRole;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repo;
        private readonly IUserRepository _userRepository;
        public UserRoleService(IUserRoleRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddUserRole(AddUserRole userRole, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var addUserRole = new UserRole
            {
                RoleId = userRole.RoleId,
                UserId = currentUserId
            };

            await _repo.InsertAsync(addUserRole);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteUserRole(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var getUserRoleById =  await _repo.GetByIdAsync(id);

            getUserRoleById.IsActive = false;
            getUserRoleById.DeletedDate = DateTime.Now;
            getUserRoleById.CreatedBy = currentUserId;
            await _repo.UpdateAsync(getUserRoleById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<List<GetUserRole>>> GetUserRolesAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetUserRole>>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }
            
            var getUserRoleList = await _repo.GetAllAsync(p => p.IsActive == true);

            var userRoleList =  new List<GetUserRole>();
           
            foreach (var userRole in getUserRoleList)
            {
                var addUserRole = new GetUserRole
                {
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    Id = userRole.Id
                };
                userRoleList.Add(addUserRole);
            }

            return ApiResponse<List<GetUserRole>>.Success(StatusCodes.Status200OK,userRoleList);
        }

        public async Task<ApiResponse<NoData>> UpdateUserRoleAsync(UpdateUserRole updateRole, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var update = new UserRole
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                UserId = updateRole.UserId,
                RoleId = updateRole.RoleId,
                Id = updateRole.Id

            };

            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
