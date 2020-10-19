using GaripSozluk.Business.Interfaces;
using GaripSozluk.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GaripSozluk.Business.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggerMiddleware>();
        }

        public async Task Invoke(HttpContext context, ILogService logService)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                //TraceIdentifier,
                //ResponseStatusCode, 
                //RequestMethod, 
                //RequestPath, 
                //UserAgent,
                //RoutePath,
                //IPAddress

                var routePath = "";

                var routeDataValues = context.GetRouteData().Values;

                if (routeDataValues.Count > 0)
                {
                    foreach (var item in routeDataValues)
                    {
                        routePath += "/" + item.Key + ": " + item.Value;
                    }
                }
                else
                {
                    routePath = "/";
                }

                var log = new Log()
                {
                    TraceIdentifier = context.TraceIdentifier,
                    ResponseStatusCode = context.Response.StatusCode,
                    RequestMethod = context.Request.Method,
                    RequestPath = context.Request.Path,
                    UserAgent = context.Request.Headers["User-Agent"],
                    RoutePath = routePath,
                    IPAddress = context.Connection.RemoteIpAddress.ToString(),
                    CreateDate = DateTime.Now
                };

                logService.InsertLog(log);
            }
        }
    }
}
