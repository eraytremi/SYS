using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity;
using Entity.Dtos.User;
using Infrastructure.Exceptions;
using Infrastructure.Utilities.Responses;
using Infrastructure.Utilities.Security.Hashing;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }


      
        public async Task<ApiResponse<NoData>> AddUser(RegisterDto dto, long currentUserId)
        {
            var user = new User
            {
                Name = dto.Name,
                CreatedBy=currentUserId,
                CreatedDate=DateTime.Now,
                Mail=dto.Mail,
                IsActive=true           
            };
         
            (user.PasswordHash, user.PasswordSalt) = HashingHelper.CreatePassword(dto.Password);
            
            await _repo.InsertAsync(user);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);

        }

        public async Task<ApiResponse<NoData>> DeleteUser(int id, long currentUserId)
        {
            var getUser = await _repo.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }
            var user = await _repo.GetByIdAsync(id);

            user.DeletedDate = DateTime.UtcNow;
            user.IsActive = false;
            await _repo.UpdateAsync(user);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public  async Task<ApiResponse<List<GetUser>>> GetUsers(long currentUserId)
        {
            var getUser = await _repo.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetUser>>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var getAll =  await _repo.GetAllAsync(p=>p.IsActive==true);
            var mappingUsers = new List<GetUser>();
            foreach (var item in getAll)
            {
                var a = new GetUser
                {
                    Id = item.Id,
                    Mail = item.Mail,
                    Name = item.Name
                    
                };
                mappingUsers.Add(a);
            }
           
            return ApiResponse<List<GetUser>>.Success(StatusCodes.Status200OK, mappingUsers);
        }

        public async Task<ApiResponse<GetUser>> LoginUser(LoginDto dto)
        {
          
            var getUser = await _repo.GetAsync(p => p.Mail == dto.Mail);
            if (getUser==null)
            {                   
                throw new BadRequestException("kullanıcı bulunamadı");
            }
            if (!HashingHelper.VerifyPasswordHash(dto.Password, getUser.PasswordHash, getUser.PasswordSalt))
                throw new BadRequestException("Parola hatalı");

            var mappingUser = new GetUser
            {
                Id = getUser.Id,
                Mail = getUser.Mail,
                Name = getUser.Name,
            };
            return ApiResponse<GetUser>.Success(StatusCodes.Status200OK, mappingUser);
        }

        public async Task<ApiResponse<NoData>> UpdateUser(UpdateUser dto, long currentUserId)
        {
            var getUser = await _repo.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var update = new User
            {
                Id = dto.Id,
                Mail = dto.Mail,
                Name = dto.Name,
                IsActive=true
               
            };

            (update.PasswordHash, update.PasswordSalt) = HashingHelper.CreatePassword(dto.Password);
            update.UpdatedDate =DateTime.Now;
            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
