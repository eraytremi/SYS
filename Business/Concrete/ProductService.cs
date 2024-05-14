using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.Product;
using Entity.Dtos.Supplier;
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
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IProductRepository repo, IUserRepository userRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
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
                IsActive = true,
                Name = product.Name,
                Description = product.Description,
                Unit = product.Unit,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
                Price = product.Price,
                SeriNo = product.SeriNo
                
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

            var query = from product in await _repo.GetAllAsync()
                        join supplier in await _supplierRepository.GetAllAsync()
                        on product.Id equals supplier.Id
                        join category in await _categoryRepository.GetAllAsync()
                        on product.Id equals category.Id
                        where product.IsActive == true
                        select new GetProduct
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,                           
                            Price = product.Price,
                            SeriNo = product.SeriNo,
                            Unit = product.Unit,
                            CategoryId = product.CategoryId,
                            SupplierId = product.SupplierId,
                            GetCategory = new GetCategory
                            {
                                Id = category.Id,
                                Name = category.Name
                            },
                            GetSupplier = new GetSupplier
                            {
                                Name = supplier.Name,
                                Id = supplier.Id
                            }
                        };
            var list =  query.ToList();

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
                
                Description = updateProduct.Description,
                Id = updateProduct.Id,
                Name = updateProduct.Name,
                CategoryId= updateProduct.CategoryId,
                Unit=updateProduct.Unit,
                Price = updateProduct.Price,
                SupplierId = updateProduct.SupplierId,
                SeriNo=updateProduct.SeriNo,
                UpdatedBy = currentUserId,
                UpdatedDate=DateTime.Now

            };
             
            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
