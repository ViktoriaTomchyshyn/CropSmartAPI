using CropSmartAPI.Core.Services;
using CropSmartAPI.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CropSmartAPI.Core.Filters;

public class AccessCheckFilter : IActionFilter
{
    private readonly ISessionControlService _sessionControlService;

    public AccessCheckFilter(ISessionControlService sessionControlService)
    {
        _sessionControlService = sessionControlService;
    }

    public bool AllowMultiple => throw new NotImplementedException();

    public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    {
        //_sessionControlService.IsLoggedIn(actionContext.Request.Headers..ToString());
        if (actionContext.Request.Headers.TryGetValues("Key", out var headerValues))
        {
            string key = headerValues?.FirstOrDefault();
            if (!string.IsNullOrEmpty(key))
            {
                // Check if the key is registered
                bool isRegistered = _sessionControlService.IsLoggedIn(key).Result;

                if (isRegistered)
                {
                    // Key is registered, allow the request to continue
                    return continuation();
                }
                else
                {
                    // Key is not registered, return a Forbidden response
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("Unauthorized access")
                    });
                }
            }
        }

        // If the header is missing or empty, return BadRequest
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Missing or empty key")
        });
    }
}
