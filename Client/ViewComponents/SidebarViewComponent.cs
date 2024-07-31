using Client.ApiServices.Interfaces;
using Client.Models;
using Client.Models.Dtos.Chat;
using Client.Models.Dtos.User;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Client.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IHttpApiService _httpApiService;

        public SidebarViewComponent(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var personUser = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var userRoleId = HttpContext.Session.GetString("UserRole");
            var responseChat = await _httpApiService.GetDataAsync<ResponseBody<List<GetChat>>>("/Chats", personUser.Token);

            var model = new SideBarViewModel
            {
                UserGetDto = personUser,
                RoleId = userRoleId,
                GetChats=responseChat.Data
            };

            ViewData["role"]=model;

            return View(model);
        }
    }


}
