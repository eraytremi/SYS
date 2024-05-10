using Business.Abstract;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity.Dtos.SupplierProduct;
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
    public class SupplierProductService : ISupplierProductService
    {
        private readonly ISupplierProductRepository _repo;
        private readonly IUserRepository _userRepository;
        public SupplierProductService(ISupplierProductRepository repo, IUserRepository userRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<NoData>> AddSupplierProduct(PostSupplierProduct dto, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new SupplierProduct { 
                ProductId = dto.ProductId, 
                ProductName = dto.ProductName,
                CreatedBy=currentUserId,
                CreatedDate=DateTime.Now,
                IsActive=true ,
                SupplierName = dto.SupplierName,
            };
            
            await _repo.InsertAsync(add);

            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);


        }

        public async Task<ApiResponse<NoData>> DeleteSupplierProductAsync(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById =  await _repo.GetByIdAsync(id);
            getById.IsActive = false;
            getById.DeletedDate= DateTime.Now;
            getById.DeletedBy = currentUserId;

            await _repo.UpdateAsync(getById);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetSupplierProduct>>> GetSupplierProductAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetSupplierProduct>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var list = new List<GetSupplierProduct>();
            var getList = await _repo.GetAllAsync(p => p.IsActive == true);

            foreach (var item in getList)
            {
                var add = new GetSupplierProduct
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    SupplierId = item.SupplierId,
                    SupplierName = item.SupplierName
                };
                list.Add(add);
            }
            return ApiResponse<List<GetSupplierProduct>>.Success(StatusCodes.Status200OK,list);

        }

        public async Task<ApiResponse<NoData>> UpdateSupplierProductAsync(UpdateSupplierProduct supplierProduct, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new SupplierProduct
            {
                ProductId = supplierProduct.ProductId,
                ProductName = supplierProduct.ProductName,
                SupplierId = supplierProduct.SupplierId,
                SupplierName = supplierProduct.SupplierName
            };

            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
