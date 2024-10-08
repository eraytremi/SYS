﻿using Client.ApiServices.Interfaces;
using Client.Filters;
using Client.Models;
using Client.Models.Dtos.Category;
using Client.Models.Dtos.Email;
using Client.Models.Dtos.Supplier;
using Client.Models.Dtos.User;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client.Controllers
{
    [SessionAspect]
    public class SupplierController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public SupplierController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetSupplier>>>("/Suppliers", token.Token);
            var responseCategory = await _httpApiService.GetDataAsync<ResponseBody<List<GetCategory>>>("/Categories", token.Token);
            
            var vm = new SupplierCategoryVm
            {
                Category = responseCategory.Data,
                Supplier = response.Data
            };

            return View(vm);
        }

       

        [HttpPost]
        public async Task<IActionResult> PostSupplier(PostSupplier dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostSupplier>>("/Suppliers", JsonSerializer.Serialize(dto), token.Token);
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
        public async Task<IActionResult> SendEmail([FromBody]RequestEmail dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<RequestEmail>("/Emails/send", JsonSerializer.Serialize(dto), token.Token);
            if (response != null)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Kaydedildi", response });
            }
            else
            {
                return Json(new { IsSuccess = false });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplier dto)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateSupplier>>("/Suppliers", JsonSerializer.Serialize(dto), token.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Başarıyla Güncellendi", response.Data });

            }

            else
            {
                return Json(new { IsSuccess = false, Messages = response.ErrorMessages });

            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Suppliers/{id}", token.Token);

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
