using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.Product;
using Entity.Dtos.StockStatus;
using Entity.Dtos.Supplier;
using Entity.Dtos.WareHouse;
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
    public class StockStatusService : IStockStatusService
    {
        private readonly IStockStatusRepository _stockStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWareHouseRepository _warehouseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        public StockStatusService(IStockStatusRepository stockStatusRepository, IUserRepository userRepository, IProductRepository productRepository, IWareHouseRepository warehouseRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _stockStatusRepository = stockStatusRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task<ApiResponse<NoData>> AddAsync(PostStockStatus postStockStatus, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new StockStatus
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = postStockStatus.ProductId,
                Quantity = postStockStatus.Quantity

            };

            await _stockStatusRepository.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);

        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _stockStatusRepository.GetByIdAsync(id);
            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            getById.IsActive = false;
            await _stockStatusRepository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetStockStatus>>> GetAllAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStockStatus>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _stockStatusRepository.GetAllAsync();
            var list = new List<GetStockStatus>();
           
            foreach (var item in getList)
            {
                var getProduct = await _productRepository.GetByIdAsync(item.ProductId);
                var getWareHouseId = await _warehouseRepository.GetByIdAsync((int)getProduct.WareHouseId);
                var getCategoryById = await _categoryRepository.GetByIdAsync(getProduct.CategoryId);
                var getSupplierById = await  _supplierRepository.GetByIdAsync(getProduct.SupplierId);
                var add = new GetStockStatus
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    GetProduct = new GetProduct
                    {
                        Id=item.ProductId,
                        WareHouseId=getProduct.WareHouseId,
                        Name = getProduct.Name,
                        Price = getProduct.Price,
                        SupplierId = getProduct.SupplierId,
                        CategoryId = getProduct.CategoryId,
                        Unit = ConvertUnitEnumToString(getProduct.Unit),
                        Description = getProduct.Description,
                        GetWareHouse = new GetWareHouse
                        {
                            Id = getWareHouseId.Id,
                            Name = getWareHouseId.Name
                        },
                        GetSupplier = new GetSupplier
                        {
                            Id = getSupplierById.Id,
                            Description = getSupplierById.Description,
                            Name = getSupplierById.Name
                        },
                        GetCategory= new GetCategory
                        {
                            Id= getCategoryById.Id,
                            Name = getCategoryById.Name
                        }
                    }
                };
                list.Add(add);
            }

            return ApiResponse<List<GetStockStatus>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(UpdateStockStatus updateStockStatus, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new StockStatus
            {
                Id = updateStockStatus.Id,
                ProductId = updateStockStatus.ProductId,
                Quantity = updateStockStatus.Quantity,
            };

            await _stockStatusRepository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
        public string ConvertUnitEnumToString(Unit unit)
        {

            switch (unit)
            {
                case Unit.Kg:
                    return "Kilogram";
                case Unit.Litre:
                    return "Litre";
                case Unit.Adet:
                    return "Adet";

                default:
                    return unit.ToString();
            }
        }

    }
}
