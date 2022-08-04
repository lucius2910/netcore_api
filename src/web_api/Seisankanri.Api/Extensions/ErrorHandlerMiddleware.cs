using Business.Core.Extensions;
using Business.Core.Interfaces;
using Business.Logs.Interfaces;
using FluentValidation;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Framework.Core.Helpers;
using System.Net;
using System.Text.Json;

namespace Seisankanri.Api.Extensions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ErrorHandlerMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context, ILocalizeServices ls)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException vex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.OK;

                var result = JsonSerializer.Serialize(vex.ToResponse());
                await response.WriteAsync(result);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                // Write log exception
                if (error != null)
                {
                    string path = context.Request.Path;
                    string action = context.Request.Method;
                    // using (var scope = _serviceScopeFactory.CreateScope())
                    // {
                    //     var logService = scope.ServiceProvider.GetService<ILogService>();
                    //     if (logService != null) await logService.WriteLogException($"{action} - {path}", error.Message, error.StackTrace);
                    // }
                }
                //var result = JsonSerializer.Serialize(new BaseResponseError(ResponseCode.SystemError, error?.Message));
                var result = JsonSerializer.Serialize(new BaseResponseError(ResponseCode.SystemError, ls.Get(Modules.CORE, "Message", MessageKey.BE_003)));
                await response.WriteAsync(result);
            }
        }
    }
}
