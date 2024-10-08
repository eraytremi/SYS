﻿using Business.Abstract;
using Entity.Dtos.Product;
using Entity.Dtos.Role;
using Infrastructure.Consts;
using Infrastructure.CustomAttributes;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _service;

        public ProductsController(IConfiguration configuration, IProductService service)
        {
            _configuration = configuration;
            _service = service;
        }


        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConsts.Product, ActionType = ActionType.Reading, Definition = "Get Products")]
        public async Task<IActionResult> GetAll()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetProductAsync(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost("getProductsByBarcode")]
        public async Task<IActionResult> GetProductByBarcode(string barcode)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetProductByBarcode(barcode,currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }


        [HttpPost("getProductsById")]
        public async Task<IActionResult> GetProductsById([FromBody]long id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.GetProductById(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody]AddProduct dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.AddProductAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.UpdateProductAsync(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _service.DeleteProductAsync(id, currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }
    }
}
