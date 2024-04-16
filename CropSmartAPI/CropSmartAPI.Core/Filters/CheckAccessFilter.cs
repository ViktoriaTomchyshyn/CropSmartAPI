using CropSmartAPI.Core.Services.Interfaces;
using System.Net;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CropSmartAPI.Core.Filters;

public class AccessCheckFilter : IAsyncActionFilter
{
    private readonly ISessionControlService _sessionControlService;

    public AccessCheckFilter(ISessionControlService sessionControlService)
    {
        _sessionControlService = sessionControlService;
    }

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Headers.TryGetValue("key", out var headerValues))
        {
            string key = headerValues.FirstOrDefault();
            if (!string.IsNullOrEmpty(key))
            {
                // Check if the key is registered
                bool isRegistered =  _sessionControlService.IsLoggedIn(key).Result;

                if (isRegistered)
                {
                    // Key is registered, allow the request to continue
                    return next();
                }
            }
        } 

        // If the header is missing or empty, return Forbidden
        context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        return Task.CompletedTask;
    }
}

