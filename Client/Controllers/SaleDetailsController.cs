using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.SalesDetails;
using Client.Models.Dtos.StockStatus;
using Client.Models.Dtos.User;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    public class SaleDetailsController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public SaleDetailsController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var product = await _httpApiService.GetDataAsync<ResponseBody<List<GetProduct>>>("/Products", token.Token);
            return View(product.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesCustomerVm dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<string>>("/SalesDetails", JsonSerializer.Serialize(dto), token.Token);

            if (response.StatusCode == 201)
            {
                var pdfPath = response.Data;
                if (System.IO.File.Exists(pdfPath))
                {
                    return Json(new { IsSuccess = true, Message = "Başarıyla Kaydedildi", FilePath = pdfPath });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "PDF dosyası bulunamadı" });
                }
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }




        [HttpGet]
        public IActionResult Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound("Dosya bulunamadı.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);

           
            return File(fileBytes, "application/pdf", fileName);
        }


    }
}
