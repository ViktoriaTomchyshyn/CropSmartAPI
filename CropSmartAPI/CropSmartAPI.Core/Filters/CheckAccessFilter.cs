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
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AccessCheckFilter(ISessionControlService sessionControlService, IHttpContextAccessor httpContextAccessor)
    {
        _sessionControlService = sessionControlService;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var headerValues))
        {
            string key = headerValues.FirstOrDefault();
            if (!string.IsNullOrEmpty(key))
            {
                // Check if the key is registered
                bool isRegistered = _sessionControlService.IsLoggedIn(key.Split(' ')[1]).Result;

                if (isRegistered)
                {
                    /// Перевірити, чи об'єкт HttpContext не є порожнім
                    if (_httpContextAccessor.HttpContext != null)
                    {
                        // Додати або оновити значення у HttpContext.Items
                        _httpContextAccessor.HttpContext.Items["UserId"] = _sessionControlService.GetUserIdByKey(key.Split(' ')[1]).Result;
                    }
                    return next();
                }
            }
        }

        // If the header is missing or empty, return Forbidden
        context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);

        return Task.CompletedTask;
    }
}