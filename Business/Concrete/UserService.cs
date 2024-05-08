using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity;
using Entity.Dtos.User;
using Infrastructure.Exceptions;
using Infrastructure.Utilities.Responses;
using Infrastructure.Utilities.Security.Hashing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }


      
        public async Task<ApiResponse<NoData>> AddUser(RegisterDto dto, int currentUserId)
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
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<NoData>> DeleteUser(int id, int currentUserId)
        {
            var getUser = await _repo.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }
            
            getUser.DeletedDate = DateTime.UtcNow;
            getUser.IsActive = false;
            await _repo.UpdateAsync(getUser);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public  async Task<ApiResponse<List<GetUser>>> GetUsers(int currentUserId)
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
                    Id = currentUserId,
                    Mail = item.Mail,
                    Name = item.Name,
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
                throw new BadRequestException("Email veya parola hatalı");

            var mappingUser = new GetUser
            {
                Id = getUser.Id,
                Mail = getUser.Mail,
                Name = getUser.Name,
            };
            return ApiResponse<GetUser>.Success(StatusCodes.Status200OK, mappingUser);
        }

        public async Task<ApiResponse<NoData>> UpdateUser(UpdateUser dto, int currentUserId)
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
                Name = dto.Name
                
            };

            (update.PasswordHash, update.PasswordSalt) = HashingHelper.CreatePassword(dto.Password);
            update.UpdatedDate =DateTime.Now;
            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
