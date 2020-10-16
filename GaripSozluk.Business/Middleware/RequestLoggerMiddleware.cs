using GaripSozluk.Business.Interfaces;
using GaripSozluk.Data.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

                if (context.Request.Path.HasValue)
                {
                    routePath += context.Request.Path.Value; // string.Join("/", context.RouteData.Values.Values);
                }

                if (context.Request.QueryString.HasValue)
                {
                    routePath += context.Request.QueryString.Value;
                }

                var log = new Log()
                {
                    TraceIdentifier = context.TraceIdentifier,
                    ResponseStatusCode = context.Response.StatusCode.ToString(),
                    RequestMethod = context.Request.Method,
                    RequestPath = context.Request.Path,
                    UserAgent = context.Request.Headers["User-Agent"],
                    RoutePath = routePath,
                    IPAddress = context.Connection.RemoteIpAddress.ToString(),
                };

                logService.InsertLog(log);
            }
        }
    }
}
