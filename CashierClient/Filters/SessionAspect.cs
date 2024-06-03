﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Client.Filters
{
    public class SessionAspect : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionJson = context.HttpContext.Session.GetString("ActivePerson");
            if (string.IsNullOrEmpty(sessionJson))
            {
                context.Result = new RedirectResult("~/Auth/Index");
            }
        }
    }
}
