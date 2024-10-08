﻿using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Supplier;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;
        private readonly IUserRepository _userRepository;
        public SupplierService(ISupplierRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddSupplierAsync(AddSupplier supplier, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new Supplier
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                Name = supplier.Name,
                IsActive=true,
                Description = supplier.Description,
                Category = supplier.Category,
                Mail=supplier.Mail
            };

            await _repo.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteSupplierAsync(int id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _repo.GetByIdAsync(id);
            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            getById.IsActive = false;
            await _repo.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

       

        public async Task<ApiResponse<List<GetSupplier>>> GetSupplierAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetSupplier>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList =  await _repo.GetAllAsync(p=>p.IsActive==true);
            var list =  new List<GetSupplier>();
            
           
                foreach (var item in getList)
                {
                    var add = new GetSupplier
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Category = item.Category,
                        Mail = item.Mail
                    };
                    list.Add(add);
                }

                return ApiResponse<List<GetSupplier>>.Success(StatusCodes.Status200OK, list);
            
           
        }

        public async Task<ApiResponse<NoData>> UpdateSupplierAsync(UpdateSupplier supplier, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new Supplier
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                Name = supplier.Name,
                Id= supplier.Id,
                Description = supplier.Description,
                IsActive=true,
                Category = supplier.Category,
                Mail = supplier.Mail
            };

            await   _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
