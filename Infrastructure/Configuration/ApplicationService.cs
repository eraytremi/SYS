using Entity.Dtos.Configurations;
using Infrastructure.CustomAttributes;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Configuration
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));


            List<Menu> menus = new();
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                    if (actions != null)
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Menu menu = null;

                                var authorizeDefinitioAtributes = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                                if (!menus.Any(m => m.Name == authorizeDefinitioAtributes.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitioAtributes.Menu };
                                    menus.Add(menu);

                                }
                                else
                                {
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitioAtributes.Menu);

                                }
                                Entity.Dtos.Configurations.Action _action = new()
                                {

                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitioAtributes.ActionType),
                                    Definition = authorizeDefinitioAtributes.Definition
                                };

                                var httpAtributes = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAtributes != null)
                                {
                                    _action.HttpType = httpAtributes.HttpMethods.First();
                                }
                                else
                                {
                                    _action.HttpType = HttpMethods.Get;
                                }

                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";
                                menu.Actions.Add(_action);

                            }

                        }

                }

            }

            return menus;



        }
    }
}
