using Aspose.Pdf;
using Aspose.Pdf.Text;
using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Customer;
using Entity.Dtos.Sales;
using Entity.Dtos.SalesDetails;
using Entity.Dtos.StockStatus;
using Entity.SysModel;
using Entity.ViewModels;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class SaleDetailsService:ISaleDetailsService
    {
        private readonly ISalesDetailsRepository _salesDetailsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly ICustomerService _customerService;
        private readonly IStockService _stockService;
        public SaleDetailsService(ISalesDetailsRepository salesDetailsRepository, IUserRepository userRepository, IProductRepository productRepository, ISalesRepository salesRepository, ICustomerService customerService, IStockService stockService)
        {
            _salesDetailsRepository = salesDetailsRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _salesRepository = salesRepository;
            _customerService = customerService;
            _stockService = stockService;
        }

        public async Task<ApiResponse<string>> AddSalesDetailsAsync(SalesCustomerVM vm, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<string>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            await _customerService.AddCustomer(vm.Customer, currentUserId);
            var getCustomer = await _customerService.GetCustomers(currentUserId);
            var lastIndexCustomer = getCustomer.Data.OrderByDescending(x => x.Id).FirstOrDefault();

            decimal totalAmount = 0;

            foreach (var item in vm.SalesDetails)
            {
                decimal amount = item.Price * (decimal)item.Quantity;
                totalAmount += amount;
            }

            var addSales = new Sales
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                IsActive = true,
                Date = DateTime.Now,
                TotalAmount = totalAmount,
                CustomerId=lastIndexCustomer.Id
            };

            await _salesRepository.InsertAsync(addSales);

            var getSales =  await _salesRepository.GetAllAsync(p=>p.IsActive==true);
            var lastIndex = getSales.OrderByDescending(x => x.Id).FirstOrDefault();

            foreach (var sale in vm.SalesDetails)
            {
                var product = await _productRepository.GetByIdAsync(sale.ProductId );
                var stock = await _stockService.GetByProductIdAsync(product.Id,currentUserId);
                if (stock.Data==null)
                {
                    return ApiResponse<string>.Fail(StatusCodes.Status400BadRequest, $"Stokta yeterli {product.Name} yok ");
                }

                if (sale.Quantity > stock.Data.Quantity )
                {
                    return ApiResponse<string>.Fail(StatusCodes.Status400BadRequest, $"Stokta yeterli {product.Name} yok ");
                }

                var add = new SalesDetails
                {
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    Price = product.Price,
                    Quantity = sale.Quantity,
                    ProductId = sale.ProductId,
                    SalesId = lastIndex.Id,
                    IsActive=true
                };

                await _salesDetailsRepository.InsertAsync(add);

                var stockStatus = new UpdateStock
                {   
                    ProductId = product.Id,
                    Quantity =stock.Data.Quantity-sale.Quantity,
                    Id = stock.Data.Id,
                };

                await _stockService.UpdateAsync(stockStatus,currentUserId);
            }

            //fatura oluştur

            var getSaleDetails = await _salesDetailsRepository.GetAllAsync(p => p.IsActive == true && p.SalesId == lastIndex.Id);
            var salesDetails = getSaleDetails.Select(sd => new GetSalesDetails
            {
                SalesId = lastIndex.Id,
                Id = sd.Id,
                Price = sd.Price,
                ProductId = sd.ProductId,
                Quantity = sd.Quantity
            }).ToList();

            var sales = new GetSales
            {
                Date = addSales.Date,
                Id = lastIndex.Id,
                TotalAmount = addSales.TotalAmount,
                SalesDetails = salesDetails,
                Customer = new GetCustomer
                {
                    Name = vm.Customer.Name,
                    CompanyName = vm.Customer.CompanyName,
                    PhoneNumber = vm.Customer.PhoneNumber,
                    Mail = vm.Customer.Mail,
                    Address = vm.Customer.Address
                }
            };

            var filePath= await CreateBillPdf(sales);




            return ApiResponse<string>.Success(StatusCodes.Status201Created,filePath);
        }

        public async Task<ApiResponse<NoData>> DeleteSalesDetailsAsync(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getById =await _salesDetailsRepository.GetByIdAsync(id);

            getById.DeletedBy = currentUserId;
            getById.DeletedDate = DateTime.Now;
            getById.IsActive = false;
            await _salesDetailsRepository.UpdateAsync(getById);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetSalesDetails>>> GetSalesDetailsAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetSalesDetails>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList = await _salesDetailsRepository.GetAllAsync(p=>p.IsActive==true);
            var list =  new List<GetSalesDetails>();

            foreach (var item in getList)
            {
                var add = new GetSalesDetails
                {
                    Id = item.Id,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SalesId=item.SalesId,
                };
                list.Add(add);  
            }
            return ApiResponse<List<GetSalesDetails>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateSalesDetailsAsync(UpdateSalesDetails update, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var updateDetails = new SalesDetails
            {
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,
                ProductId = update.ProductId,
                SalesId = update.SalesId,
                Price = update.Price,
                Quantity = update.Quantity,
                IsActive = true,
                Id = update.Id

            };
             
            await _salesDetailsRepository.UpdateAsync(updateDetails);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<string> CreateBillPdf(GetSales sales)
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

           
            var tempPath = Path.Combine(Path.GetTempPath(), $"fatura_{sales.Id}.pdf");
            document.Save(tempPath);

            return tempPath;
        }

    }
}
