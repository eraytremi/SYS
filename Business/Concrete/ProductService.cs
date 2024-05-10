using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Product;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IUserRepository _userRepository;
        public ProductService(IProductRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddProductAsync(AddProduct product, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new Product
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                Count = product.Count,
                IsActive = true,
                Name = product.Name,
                Description = product.Description,
                Unit = product.Unit
            };
            await _repo.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteProductAsync(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _repo.GetByIdAsync(id);

            getById.IsActive = false;
            getById.DeletedDate= DateTime.Now;
            getById.DeletedBy = currentUserId;

            await _repo.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetProduct>>> GetProductAsync(int currentUserId)
        {   
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetProduct>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList =  await _repo.GetAllAsync(p => p.IsActive == true);
            var list =  new List<GetProduct>();

            foreach (var item in getList)
            {
                var add = new GetProduct
                {
                    Count = item.Count,
                    Description = item.Description,
                    Id = item.Id,
                    Name = item.Name
                };
                list.Add(add);  
            }

            return ApiResponse<List<GetProduct>>.Success(StatusCodes.Status200OK,list);
        }

        public async Task<ApiResponse<NoData>> UpdateProductAsync(UpdateProduct updateProduct, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new Product
            {
                Count = updateProduct.Count,
                Description = updateProduct.Description,
                Id = updateProduct.Id,
                Name = updateProduct.Name
            };
             
            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
