using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos;
using Client.Models.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Text.Json;

namespace Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public CategoryController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            

            var category = await _httpApiService.GetDataAsync<ResponseBody<List<GetCategory>>>("/Categories", token.Token);

            foreach (var categoryItem in category.Data)
            {
                if (categoryItem.Picture != null)
                {
                    categoryItem.PictureBase64 = Convert.ToBase64String(categoryItem.Picture);
                }
            }
          
            return View(category.Data);
        }


        [HttpGet]
        public async Task<IActionResult> CategoryMenu()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");


            var category = await _httpApiService.GetDataAsync<ResponseBody<List<GetCategory>>>("/Categories", token.Token);

            foreach (var categoryItem in category.Data)
            {
                if (categoryItem.Picture != null)
                {
                    categoryItem.PictureBase64 = Convert.ToBase64String(categoryItem.Picture);
                }
            }
            ViewData["CategoriMenu"]=category.Data;
            return View(category.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostCategory dto, IFormFile Picture)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            if (Picture != null && Picture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Picture.CopyToAsync(memoryStream);
                    using (var image = Image.Load(memoryStream.ToArray()))
                    {
                     
                        int desiredWidth = 300; 
                        int desiredHeight = 300;

                        image.Mutate(x => x.Resize(desiredWidth, desiredHeight));

                      
                        memoryStream.SetLength(0);
                        image.Save(memoryStream, new JpegEncoder()); 

                       
                        dto.Picture = memoryStream.ToArray();
                    }
                }
            }

            var response = await _httpApiService.PostDataAsync<ResponseBody<PostCategory>>("/Categories", JsonSerializer.Serialize(dto), token.Token);

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
        public async Task<IActionResult> Update(UpdateCategory dto, IFormFile Picture)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            if (Picture != null && Picture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Picture.CopyToAsync(memoryStream);
                    using (var image = Image.Load(memoryStream.ToArray()))
                    {
                        
                        int desiredWidth = 300; 
                        int desiredHeight = 300;

                        image.Mutate(x => x.Resize(desiredWidth, desiredHeight));

                     
                        memoryStream.SetLength(0); 
                        image.Save(memoryStream, new JpegEncoder()); 

                     
                        dto.Picture = memoryStream.ToArray();
                    }
                }
            }
            var response = await _httpApiService.PutDataAsync<ResponseBody<UpdateCategory>>("/Categories", JsonSerializer.Serialize(dto), token.Token);
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
        public async Task<IActionResult> Delete(long id)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoContent>>($"/Categories/{id}", token.Token);

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
