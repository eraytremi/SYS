﻿using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Category;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IUserRepository _userRepository;

        public CategoryService(ICategoryRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddCategory(AddCategory category, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var add = new Category
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                Picture=category.Picture,
                Name = category.Name,
                IsActive=true
            };
            await _repo.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteCategory(int id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _repo.GetByIdAsync(id);
            getById.IsActive = false;
            getById.DeletedDate = DateTime.Now;
            getById.DeletedBy = currentUserId;
            await _repo.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetCategory>>> GetCategoriesAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetCategory>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _repo.GetAllAsync(p => p.IsActive == true);
            var list = new List<GetCategory>();
            foreach (var item in getList)
            {
                var addList = new GetCategory
                {
                    Id = item.Id,
                    Name = item.Name,
                    Picture=item.Picture
                };
                list.Add(addList);

            }

            return ApiResponse<List<GetCategory>>.Success(StatusCodes.Status200OK, list);
        }

        public  async Task<ApiResponse<NoData>> UpdateCategoryAsync(UpdateCategory updateCategory, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new Category
            {
                UpdatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                Name = updateCategory.Name,
                Picture=updateCategory.Picture,
                Id = updateCategory.Id,
                IsActive = true
            };
            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            

        }
    }
}
