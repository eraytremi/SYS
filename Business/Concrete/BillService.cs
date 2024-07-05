using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Bill;
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
    public class BillService : IBillService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBillRepository _billRepository;
        private readonly ISaleService _saleService;
        public BillService(IUserRepository userRepository, IBillRepository billRepository, ISaleService saleService)
        {
            _userRepository = userRepository;
            _billRepository = billRepository;
            _saleService = saleService;
        }

        public async Task<ApiResponse<NoData>> AddBillAsync(PostBill bill, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var getSales = await _saleService.GetSales(bill.SalesId, currentUserId);

            var add = new Bill
            {
                BillDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedBy = currentUserId,
                CustomerAdress = bill.CustomerAdress,
                CustomerName = bill.CustomerName,
                SalesId = bill.SalesId,
                TotalAmount = getSales.Data.TotalAmount,
                IsActive = true
            };
            await _billRepository.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteBillAsync(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _billRepository.GetByIdAsync(id);
            getById.IsActive = false;
            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            
            await _billRepository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetBill>>> GetBillAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetBill>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _billRepository.GetAllAsync(p=>p.IsActive==true);
            var list = new List<GetBill>();

            foreach (var item in getList)
            {
                var getSales = await _saleService.GetSales(item.SalesId, currentUserId);

                var add = new GetBill
                {
                    CustomerAdress = item.CustomerAdress,
                    SalesId = item.SalesId,
                    TotalAmount = getSales.Data.TotalAmount,
                    CustomerName = item.CustomerName,
                    BillDate = item.BillDate,
                };
                list.Add(add);
            }

            return ApiResponse<List<GetBill>>.Success(StatusCodes.Status200OK, list);
            
        }

        public async Task<ApiResponse<NoData>> UpdateBillAsync(UpdateBill bill, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new Bill
            {
                CustomerAdress = bill.CustomerAdress,
                SalesId = bill.SalesId,
                TotalAmount = bill.TotalAmount,
                CustomerName = bill.CustomerName,
                Id = bill.Id,
                UpdatedBy=currentUserId,
                UpdatedDate=DateTime.Now,
                IsActive=true
            };

            await _billRepository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
