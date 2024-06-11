using Business.Abstract;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity;
using Entity.Dtos.User;
using Entity.Dtos.UserClient;
using Entity.SysModel;
using Infrastructure.Exceptions;
using Infrastructure.Utilities.Responses;
using Infrastructure.Utilities.Security.Hashing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserClientService : IUserClientService
    {
        private readonly IUserClientRepository _repository;
      
        public UserClientService(IUserClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<NoData>> AddUserClient(PostUserClient dto, int currentUserId)
        {
            var user = new UserClient
            {
                Name = dto.Name,
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                NickName=dto.NickName,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                Surname = dto.Surname,
                IsActive = true
            };

            (user.PasswordHash, user.PasswordSalt) = HashingHelper.CreatePassword(dto.Password);

            await _repository.InsertAsync(user);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> DeleteUserClient(int id, int currentUserId)
        {
            var getUser = await _repository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            getUser.DeletedDate = DateTime.UtcNow;
            getUser.IsActive = false;
            await _repository.UpdateAsync(getUser);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetUserClient>>> GetUserClients(int currentUserId)
        {
            var getUser = await _repository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetUserClient>>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var getAll = await _repository.GetAllAsync(p => p.IsActive == true);
            var mappingUsers = new List<GetUserClient>();
            foreach (var item in getAll)
            {
                var a = new GetUserClient
                {
                    Id = currentUserId,
                    Surname = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    NickName=item.NickName,
                    Name = item.Name,
                };
                mappingUsers.Add(a);
            }

            return ApiResponse<List<GetUserClient>>.Success(StatusCodes.Status200OK, mappingUsers);
        }

        public async Task<ApiResponse<GetUserClient>> LoginUserClient(Login dto)
        {
            var getUser = await _repository.GetAsync(p => p.NickName == dto.NickName);
            if (getUser == null)
            {
                throw new BadRequestException("kullanıcı bulunamadı");
            }
            if (!HashingHelper.VerifyPasswordHash(dto.Password, getUser.PasswordHash, getUser.PasswordSalt))
                throw new BadRequestException("Parola hatalı");

            var mappingUser = new GetUserClient
            {
                Id = getUser.Id,
                NickName = dto.NickName,
                Surname=getUser.Surname,
                Name = getUser.Name,
            };
            return ApiResponse<GetUserClient>.Success(StatusCodes.Status200OK, mappingUser);
        }

        public async Task<ApiResponse<NoData>> UpdateUserClient(UpdateUserClient dto, int currentUserId)
        {
            var getUser = await _repository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yetki yok!");
            }

            var update = new UserClient
            {
                Id = dto.Id,
                Surname = dto.Surname,
                PhoneNumber = dto.PhoneNumber,
                NickName=dto.NickName,
                UpdatedBy=currentUserId,
                UpdatedDate = DateTime.UtcNow,
                Password=dto.Password,
                Name = dto.Name

            };

            (update.PasswordHash, update.PasswordSalt) = HashingHelper.CreatePassword(dto.Password);
            update.UpdatedDate = DateTime.Now;
            await _repository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
