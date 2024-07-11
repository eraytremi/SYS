using Aspose.Pdf;
using Aspose.Pdf.Text;
using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Sales;
using Entity.Dtos.SalesDetails;
using Entity.SysModel;
using Entity.ViewModels;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SalesService : ISaleService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly ISalesDetailsRepository _salesDetailsRepository;
        private readonly ICustomerService _customerService;
        private readonly IProductRepository _productRepository;
        public SalesService(IUserRepository userRepository, ISalesRepository salesRepository, ISalesDetailsRepository salesDetailsRepository, ICustomerService customerService, IProductRepository productRepository)
        {
            _userRepository = userRepository;
            _salesRepository = salesRepository;
            _salesDetailsRepository = salesDetailsRepository;
            _customerService = customerService;
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<NoData>> AddSalesAsync(PostSales sales, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var salesDetails = await _salesDetailsRepository.GetAllAsync(p => p.IsActive == true);

            decimal totalAmount = 0;
            foreach (var saleDetail in salesDetails)
            {
                decimal amount = saleDetail.Price * (decimal)saleDetail.Quantity;
                totalAmount += amount;
            }

            var a = new Sales
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                TotalAmount = totalAmount,
            };

            await _salesRepository.InsertAsync(a);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteSalesAsync(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById = await _salesRepository.GetByIdAsync(id);
           

            getById.IsActive = false;
            getById.DeletedDate = DateTime.Now;
            getById.DeletedBy = currentUserId;
            await _salesRepository.UpdateAsync(getById);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<GetSales>> GetSales(long salesId, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<GetSales>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getSales = await _salesRepository.GetAsync(p => p.IsActive == true && p.Id == salesId);
            var getListDetails =  await  _salesDetailsRepository.GetAllAsync(p=>p.IsActive==true && p.SalesId==salesId);
            var mappingDetails = getListDetails.Select(p => new GetSalesDetails
            {
                SalesId = salesId,
                Id = p.Id,
                Price = p.Price,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
            }).ToList();

            var getCustomer =await _customerService.GetCustomerById(getSales.CustomerId, currentUserId);
            var mapping = new GetSales
            {

                Date = DateTime.Now,
                Id = salesId,
                TotalAmount = getSales.TotalAmount,
                SalesDetails=mappingDetails,
                Customer= getCustomer.Data
            };
            await CreateBillPdf(mapping);
            return ApiResponse<GetSales>.Success(StatusCodes.Status200OK,mapping);
        }

        public async Task<ApiResponse<List<GetSales>>> GetSalesAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetSales>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }


            var getList = await _salesRepository.GetAllAsync(p=>p.IsActive==true);

            var list = new List<GetSales>();
            
            foreach (var item in getList)
            {
                var getListSalesDetails = await _salesDetailsRepository.GetAllAsync(p => p.IsActive == true && p.SalesId == item.Id);
                var getCustomer = await _customerService.GetCustomerById(item.CustomerId,currentUserId);
                var mappedSalesDetails = getListSalesDetails.Select(sd => new GetSalesDetails
                {
                  
                    Id = sd.Id,
                    SalesId = sd.SalesId,
                    ProductId = sd.ProductId,
                    Quantity = sd.Quantity,
                    Price = sd.Price,
                   
                }).ToList();


                var add = new GetSales
                {
                    Date = DateTime.Now,
                    Id = item.Id,
                    TotalAmount = item.TotalAmount,
                    SalesDetails= mappedSalesDetails,
                    Customer=getCustomer.Data
                };
                list.Add(add);

            }

            return ApiResponse<List<GetSales>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateSalesAsync(UpdateSales update, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var updateSales = new Sales
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                TotalAmount = update.TotalAmount,
                Date = DateTime.Now,
                IsActive=true,
            };
            await _salesRepository.UpdateAsync(updateSales);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task CreateBillPdf(GetSales sales)
        {

          
            Document document = new Document();


            Page page = document.Pages.Add();


            var title = new TextFragment("Fatura");
            title.TextState.FontSize = 20;
            title.TextState.FontStyle = FontStyles.Bold;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            page.Paragraphs.Add(title);


            page.Paragraphs.Add(new TextFragment($"Müşteri: {sales.Customer.Name}"));
            page.Paragraphs.Add(new TextFragment($"Şirket: {sales.Customer.CompanyName}"));
            page.Paragraphs.Add(new TextFragment($"Tel No: {sales.Customer.PhoneNumber}"));
            page.Paragraphs.Add(new TextFragment($"Email: {sales.Customer.Mail}"));
            page.Paragraphs.Add(new TextFragment($"Adres: {sales.Customer.Address}"));
            page.Paragraphs.Add(new TextFragment($"Tarih: {DateTime.Now}"));


            page.Paragraphs.Add(new TextFragment(" "));

     
            Table table = new Table
            {
                ColumnWidths = "100 100 100 100",
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray)
            };


            var headerRow = table.Rows.Add();
            headerRow.Cells.Add("Ürün").DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
            headerRow.Cells.Add("Fiyat").DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
            headerRow.Cells.Add("Miktar").DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
            headerRow.Cells.Add("Toplam").DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

         
            foreach (var detail in sales.SalesDetails)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);
                var row = table.Rows.Add();
                row.Cells.Add(product.Name);
                row.Cells.Add(detail.Price.ToString("C"));
                row.Cells.Add(detail.Quantity.ToString());
                row.Cells.Add((detail.Price * detail.Quantity).ToString("C"));
            }

          
            page.Paragraphs.Add(table);

            
            page.Paragraphs.Add(new TextFragment(" "));
            var total = new TextFragment($"Toplam: {sales.TotalAmount.ToString("C")}");
            total.TextState.FontSize = 14;
            total.TextState.FontStyle = FontStyles.Bold;
            total.HorizontalAlignment = HorizontalAlignment.Right;
            page.Paragraphs.Add(total);

            var filePath = $"fatura_{sales.Id}.pdf";
            document.Save(filePath);
        }
    }

}

