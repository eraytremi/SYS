using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.Product;
using Entity.Dtos.Supplier;
using Entity.Dtos.WareHouse;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IUserRepository _userRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWareHouseRepository _warehouseRepository;
        public ProductService(IProductRepository repo, IUserRepository userRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, IWareHouseRepository warehouseRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<ApiResponse<NoData>> AddProductAsync(AddProduct product, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            if (!Enum.TryParse(product.Unit, out Unit unitEnum))
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Geçersiz birim değeri");
            }
            var getProduct =  await _repo.GetAsync  (p=>p.IsActive==true && p.Name==product.Name && p.SupplierId==product.SupplierId);
            if (getProduct != null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Bu ürün bu tedarikçide kayıtlı zaten!");
            }
            var add = new Product
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
                WareHouseId = product.WareHouseId,
                Price = product.Price,
                Unit = unitEnum

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
            getById.DeletedDate = DateTime.Now;
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

           
            var getListProduct = await _repo.GetAllAsync();
            var list = new List<GetProduct>();
            foreach (var item in getListProduct)
            {
                var getSupplier = await _supplierRepository.GetByIdAsync(item.SupplierId);
                var getCategory = await _categoryRepository.GetByIdAsync(item.CategoryId);
                var getWareHouse = await _warehouseRepository.GetByIdAsync(item.WareHouseId);

               
                var add = new GetProduct
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    WareHouseId = item.WareHouseId,
                    SupplierId=item.SupplierId,
                    Name = item.Name,
                    Price = item.Price,
                    Unit = ConvertUnitEnumToString(item.Unit),
                    Description = item.Description,
                    GetCategory = new GetCategory
                    {
                        Id=getCategory.Id,
                        Name=getCategory.Name
                    },

                    GetWareHouse = new GetWareHouse
                    {
                        Id=getWareHouse.Id,
                        Name=getWareHouse.Name
                    },
                    GetSupplier = new GetSupplier
                    {
                        Id=getSupplier.Id,
                        Name=getSupplier.Name
                    }
                };
                list.Add(add);
            }   

            return ApiResponse<List<GetProduct>>.Success(StatusCodes.Status200OK, list);
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
                CategoryId = updateProduct.CategoryId,
                Unit = updateProduct.Unit,
                Price = updateProduct.Price,
                SupplierId = updateProduct.SupplierId,
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now

            };

            await _repo.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public  string ConvertUnitEnumToString(Unit unit)
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
