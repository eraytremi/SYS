using ClientDx.Filters;
using ClientDx.Models;
using ClientDx.Models.Dtos;
using ClientDx.Models.Dtos.Supplier;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace ClientDx.Controllers
{
    [SessionAspect]
    public class SupplierController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public SupplierController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<object> Get(DataSourceLoadOptions loadOptions)
        {
            var token = HttpContext.Session.GetObject<ResponseBody<UserGetDto>>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ResponseBody<List<GetSupplier>>>("/Suppliers", token.Data.Token);
            return DataSourceLoader.Load(response.Data, loadOptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var token = HttpContext.Session.GetObject<ResponseBody<UserGetDto>>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ResponseBody<PostSupplier>>("/Suppliers", JsonSerializer.Serialize(values), token.Data.Token);     
            return Ok(response.StatusMessage);
        }
    }
}
