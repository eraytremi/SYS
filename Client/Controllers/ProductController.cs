﻿using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.Category;
using Client.Models.Dtos.Product;
using Client.Models.Dtos.Supplier;
using Client.Models.Dtos.User;
using Client.Models.Dtos.WareHouse;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public ProductController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var product = await _httpApiService.GetDataAsync<ResponseBody<List<GetProduct>>>("/Products", token.Token);
            var supplier = await _httpApiService.GetDataAsync<ResponseBody<List<GetSupplier>>>("/Suppliers", token.Token);
            
            var wareHouse= await _httpApiService.GetDataAsync<ResponseBody<List<GetWareHouse>>>("/WareHouses", token.Token);

            var category = await _httpApiService.GetDataAsync<ResponseBody<List<GetCategory>>>("/Categories",token.Token);

            foreach (var productItem in product.Data)
            {
                if (productItem.Picture != null)
                {
                    productItem.PictureBase64 = Convert.ToBase64String(productItem.Picture);
                }
            }

            var data = new SupplierProductWareHouseVM
            {
                GetProducts = product.Data,
                GetSuppliers = supplier.Data,
                GetWareHouses = wareHouse.Data,
                GetCategories=category.Data
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostProduct dto,IFormFile Picture)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            if (Picture != null && Picture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Picture.CopyToAsync(memoryStream);
                    dto.Picture = memoryStream.ToArray(); 
                }
            }
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostProduct>>("/Products", JsonSerializer.Serialize(dto), token.Token);

            if (response.StatusCode == 201)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Kaydedildi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProduct dto,IFormFile Picture)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            if (Picture != null && Picture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Picture.CopyToAsync(memoryStream);
                    dto.Picture = memoryStream.ToArray();
                }
            }
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateProduct>>("/Products", JsonSerializer.Serialize(dto), token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Güncellendi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<GetProduct>>("/Products/getProductsByBarcode", JsonSerializer.Serialize(barcode), token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Güncellendi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProductById([FromBody] long id)
            {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<GetProduct>>("/Products/getProductsById", JsonSerializer.Serialize(id), token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Data = response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });
            }
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Products/{id}", token.Token);

            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Silindi", response.Data });
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });

            }
        }

    }
}
