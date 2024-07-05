using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity;
using Entity.Dtos.Category;
using Entity.Dtos.Product;
using Entity.Dtos.StockMovement;
using Entity.Dtos.StockStatus;
using Entity.Dtos.Supplier;
using Entity.Dtos.WareHouse;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWareHouseRepository _warehouseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IStockMovementService _stockMovementService;
        public StockService(IStockRepository stockStatusRepository, IUserRepository userRepository, IProductRepository productRepository, IWareHouseRepository warehouseRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, IStockMovementService stockMovementService)
        {
            _stockStatusRepository = stockStatusRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _stockMovementService = stockMovementService;
        }

        public async Task<ApiResponse<NoData>> AddAsync(PostStock postStockStatus, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var a = await _stockStatusRepository.GetAsync(p => p.ProductId == postStockStatus.ProductId);

            var anyProduct = _productRepository.GetAsync(p => p.Id == postStockStatus.ProductId);
            if (anyProduct == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status404NotFound, "Ürün kayıtlı değil. Önce ürünü kaydetmeniz gerekiyor!");
            }

            if (postStockStatus.IsEntry == true)
            {

                var postStockMovement = new PostStockMovement
                {
                    IsEntry = true,
                    ProductId = postStockStatus.ProductId,
                    Quantity = postStockStatus.Quantity,
                    Destination = postStockStatus.Destination,
                    Source = postStockStatus.Source,
                    StatusType = ConvertStatusTypeEnumToString(StatusType.Bekleyen),
                };
                await _stockMovementService.AddAsync(postStockMovement, currentUserId);

                return ApiResponse<NoData>.Success(StatusCodes.Status201Created);

            }
            else
            {
                var postStockMovement = new PostStockMovement
                {
                    IsEntry = false,
                    ProductId = postStockStatus.ProductId,
                    Quantity = postStockStatus.Quantity,
                    Destination = postStockStatus.Destination,
                    Source = postStockStatus.Source,
                    StatusType = ConvertStatusTypeEnumToString(StatusType.Bekleyen),
                };
                await _stockMovementService.AddAsync(postStockMovement, currentUserId);

                return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
            }

           

        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id, long currentUserId)
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

        public async Task<ApiResponse<List<GetStock>>> GetAllAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStock>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _stockStatusRepository.GetAllAsync();
            var list = new List<GetStock>();

            foreach (var item in getList)
            {
                var getProduct = await _productRepository.GetByIdAsync(item.ProductId);
                var getWareHouseId = await _warehouseRepository.GetByIdAsync((int)getProduct.WareHouseId);
                var getCategoryById = await _categoryRepository.GetByIdAsync(getProduct.CategoryId);
                var getSupplierById = await _supplierRepository.GetByIdAsync(getProduct.SupplierId);
                var add = new GetStock
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    GetProduct = new GetProduct
                    {
                        Id = item.ProductId,
                        WareHouseId = getProduct.WareHouseId,
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
                        GetCategory = new GetCategory
                        {
                            Id = getCategoryById.Id,
                            Name = getCategoryById.Name
                        }
                    }
                };

                list.Add(add);
            }

            return ApiResponse<List<GetStock>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(UpdateStock updateStockStatus, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new Stock
            {
                Id = updateStockStatus.Id,
                ProductId = updateStockStatus.ProductId,
                Quantity = updateStockStatus.Quantity

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

        public int ConvertStatusTypeEnumToString(StatusType statusType)
        {

            switch (statusType)
            {
                case StatusType.Onaylanan:
                    return 2;
                case StatusType.Bekleyen:
                    return 1;
                case StatusType.Reddedilmiş:
                    return 3;

                default:
                    return (int)statusType;
            }
        }

        public async Task<ApiResponse<NoData>> SellProductAsync(PostStock postStockStatus, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getByIdProduct = await _productRepository.GetByIdAsync(postStockStatus.ProductId);
            var getByIdStock = await _stockStatusRepository.GetByIdAsync(postStockStatus.ProductId);

            if (getByIdProduct == null)
            {
                ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Ürün stokta yok");
            }

            if (getByIdStock.Quantity == 0)
            {
                ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, $"Yeterli {getByIdProduct.Name} ürünü yok");
            }

            var update = new Stock
            {
                ProductId = postStockStatus.ProductId,
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                IsActive = true,
                Quantity = -postStockStatus.Quantity,
                Id = getByIdStock.Id
            };

            await _stockStatusRepository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }

}
