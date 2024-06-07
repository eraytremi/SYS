using Client.Models.Dtos;
using Client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Client.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var personUser = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var userRoleId = HttpContext.Session.GetString("UserRole");

            var model = new SideBarViewModel
            {
                UserGetDto = personUser,
                RoleId = userRoleId
            };
            ViewData["role"]=model;
            return View(model);
        }
    }


}
