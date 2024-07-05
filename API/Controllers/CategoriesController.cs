using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.Category;
using Entity.Dtos.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly ICategoryService _service;

        public CategoriesController(IConfiguration configuration, ICategoryService service)
        {
            _configuration = configuration;
            _service = service;
        }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var currentUserId = CurrentUser.Get(HttpContext);
        var response = await _service.GetCategoriesAsync(currentUserId.GetValueOrDefault());
        return SendResponse(response);
    }


    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody]AddCategory dto)
    {
        var currentUserId = CurrentUser.Get(HttpContext);
        var response = await _service.AddCategory(dto, currentUserId.GetValueOrDefault());
        return SendResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategory dto)
    {
        var currentUserId = CurrentUser.Get(HttpContext);
        var response = await _service.UpdateCategoryAsync(dto, currentUserId.GetValueOrDefault());
        return SendResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var currentUserId = CurrentUser.Get(HttpContext);
        var response = await _service.DeleteCategory(id, currentUserId.GetValueOrDefault());
        return SendResponse(response);
    }
}
}
