using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity;
using Entity.Dtos.Product;
using Entity.Dtos.StockMovement;
using Entity.Dtos.WareHouse;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWareHouseRepository _wareHouseRepository;
        public StockMovementService(IStockMovementRepository repository, IUserRepository userRepository, IProductRepository productRepository, IWareHouseRepository wareHouseRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _wareHouseRepository = wareHouseRepository;
        }

        public async Task<ApiResponse<NoData>> AddAsync(PostStockMovement postStockMovement, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            if (postStockMovement.IsEntry == true)
            {
                var add = new StockMovement
                {
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ProductId = postStockMovement.ProductId,
                    Quantity = postStockMovement.Quantity,
                    IsEntry = true,
                    Source = postStockMovement.Source,
                    Destination = postStockMovement.Destination,
                    Date = DateTime.Now,
                    StatusType = StatusType.Bekleyen
                };
                await _repository.InsertAsync(add);
                return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
            }

            var insert = new StockMovement
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                ProductId = postStockMovement.ProductId,
                Date = DateTime.Now,
                IsEntry = false,
                Source = postStockMovement.Source,
                Destination = postStockMovement.Destination,
                Quantity = postStockMovement.Quantity,
                StatusType = StatusType.Bekleyen

            };
            await _repository.InsertAsync(insert);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _repository.GetByIdAsync(id);
            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            getById.IsActive = false;
            await _repository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<List<GetStockMovement>>> GetAllAsync(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStockMovement>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _repository.GetAllAsync();
            var list = new List<GetStockMovement>();
            foreach (var item in getList)
            {
                var getProduct = await _productRepository.GetByIdAsync(item.ProductId);
                var getWareHouse = await _wareHouseRepository.GetByIdAsync(getProduct.WareHouseId);
                var add = new GetStockMovement
                {
                    Date = DateTime.Now,
                    Id = item.Id,
                    IsEntry = item.IsEntry,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Destination = item.Destination,
                    Source = item.Source,
                    GetProduct = new GetProduct
                    {
                        Id = getProduct.Id,
                        Name = getProduct.Name,
                        Description = getProduct.Description,
                        Price = getProduct.Price,
                        Unit = ConvertUnitEnumToString(getProduct.Unit),
                        GetWareHouse = new GetWareHouse
                        {
                            Id = getWareHouse.Id,
                            Name = getWareHouse.Name
                        }
                    }
                };
                list.Add(add);
            }
            return ApiResponse<List<GetStockMovement>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(UpdateStockMovement updateStockMovement, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new StockMovement
            {
                Date = DateTime.Now,
                Id = updateStockMovement.Id,
                IsEntry = updateStockMovement.IsEntry,
                ProductId = updateStockMovement.ProductId,
                Quantity = updateStockMovement.Quantity
            };
            await _repository.UpdateAsync(update);
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

        public async Task<ApiResponse<NoData>> RejectStatus(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var stockMovement = await _repository.GetAsync(p => p.Id == id);
            stockMovement.UpdatedDate = DateTime.Now;
            stockMovement.UpdatedBy = currentUserId;
            stockMovement.StatusType = StatusType.Reddedilmiş;
            await _repository.UpdateAsync(stockMovement);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> ApproveStatus(int id, int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var stockMovement = await _repository.GetAsync(p => p.Id == id);


            stockMovement.UpdatedDate = DateTime.Now;
            stockMovement.UpdatedBy = currentUserId;
            stockMovement.StatusType = StatusType.Onaylanan;
            await _repository.UpdateAsync(stockMovement);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetStockMovement>>> AprroveStatuses(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStockMovement>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var getApprove = await _repository.ApprovedStatus();

            var list = new List<GetStockMovement>();
            foreach (var item in getApprove)
            {
                var add = new GetStockMovement
                {
                    Id = item.Id,
                    Date = DateTime.Now,
                    Destination = item.Destination,
                    IsEntry = item.IsEntry,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Source = item.Source
                };
                list.Add(add);
            }

            return ApiResponse<List<GetStockMovement>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<List<GetStockMovement>>> RejectedStatuses(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStockMovement>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var reject = await _repository.RejectedStatus();
            var list = new List<GetStockMovement>();
            foreach (var item in reject)
            {
                var add = new GetStockMovement
                {
                    Id = item.Id,
                    Date = DateTime.Now,
                    Destination = item.Destination,
                    IsEntry = item.IsEntry,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Source = item.Source,
                };
                list.Add(add);
            }

            return ApiResponse<List<GetStockMovement>>.Success(StatusCodes.Status200OK, list);
        }
        public async Task<ApiResponse<List<GetStockMovement>>> WaitingStatuses(int currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetStockMovement>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var waiting = await _repository.WaitingStatus();
            var list = new List<GetStockMovement>();
            foreach (var item in waiting)
            {
                var product=await _productRepository.GetByIdAsync(item.ProductId);
                var add = new GetStockMovement
                {
                    Date = item.Date,
                    Destination = item.Destination,
                    Id = item.Id,
                    IsEntry = item.IsEntry,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Source = item.Source,
                    GetProduct=new GetProduct
                    {
                        Name= product.Name     
                    }
                };
                list.Add(add);
            }
            return ApiResponse<List<GetStockMovement>>.Success(StatusCodes.Status200OK, list);
        }
    }
}

