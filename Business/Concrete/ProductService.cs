using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.Product;
using Entity.Dtos.Supplier;
using Entity.Dtos.WareHouse;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

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
            var getProduct = await _repo.GetAsync(p => p.IsActive == true && p.Name == product.Name && p.SupplierId == product.SupplierId);
            var getProducts = await _repo.GetAllAsync();
            long getLastIndex= getProducts.Count()+1;
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
                Unit = unitEnum,
                Barcode=GenerateBarcode(getLastIndex.ToString())
            };
            await _repo.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteProductAsync(long id, int currentUserId)
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


            var getListProduct = await _repo.GetAllAsync(p=>p.IsActive==true);
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
                    SupplierId = item.SupplierId,
                    Name = item.Name,
                    Price = item.Price,
                    Unit = ConvertUnitEnumToString(item.Unit),
                    Description = item.Description,
                    GetCategory = new GetCategory
                    {
                        Id = getCategory.Id,
                        Name = getCategory.Name
                    },

                    GetWareHouse = new GetWareHouse
                    {
                        Id = getWareHouse.Id,
                        Name = getWareHouse.Name
                    },
                    GetSupplier = new GetSupplier
                    {
                        Id = getSupplier.Id,
                        Name = getSupplier.Name
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

        private string GenerateBarcode(string productId)
        {
            var barcodeWriter = new BarcodeWriter<Bitmap>
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 100,
                    Margin = 10
                }
            };

            var bitMatrix = barcodeWriter.Encode(productId);
            
            var bitmap = new Bitmap(bitMatrix.Width, bitMatrix.Height);
            for (int y = 0; y < bitMatrix.Height; y++)
            {
                for (int x = 0; x < bitMatrix.Width; x++)
                {
                    bitmap.SetPixel(x, y, bitMatrix[x, y] ? Color.Black : Color.White);
                }
            }

          
            var barcodeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Barcodes");

            if (!Directory.Exists(barcodeDir))
            {
                Directory.CreateDirectory(barcodeDir);
            }

            var barcodePath = Path.Combine(barcodeDir, $"{productId}.png");
            bitmap.Save(barcodePath, ImageFormat.Png);

            return barcodePath;
        }

        public async Task<ApiResponse<GetProduct>> GetProductByBarcode(string barcode, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<GetProduct>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getProduct = await _repo.GetAsync(p => p.Barcode == barcode);
            if (getProduct == null)
            {
               return ApiResponse<GetProduct>.Fail(StatusCodes.Status404NotFound, "Ürün bulunamadı");
            }

            var mapping = new GetProduct
            {
                Price = getProduct.Price,
                CategoryId = getProduct.CategoryId,
                Description = getProduct.Description,
                Name = getProduct.Name,
                Id = getProduct.Id,
                SupplierId = getProduct.SupplierId,
                Unit = ConvertUnitEnumToString(getProduct.Unit),
                WareHouseId = getProduct.WareHouseId
            };

           return ApiResponse<GetProduct>.Success(StatusCodes.Status200OK, mapping);
        }
    }
}
