using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace JE.Restaurant.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogError(ex, "Response has already started, error cannot be handled properly");

                    throw;
                }

                try
                {
                    context.Response.Clear();
                    context.Response.OnStarting(ClearCacheHeaders, context.Response);
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
                    {
                        ServiceName = _configuration["ServiceName"],
                        ServiceVersion = _configuration["ServiceVersion"],
                        Message = ex.GetBaseException().Message
                    }, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffK",
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                    }));

                    return;
                }
                catch (Exception ex2)
                {
                    _logger.LogError(ex2, "Error handler failed to handle error!");
                }
                throw;
            }
        }

        private class ErrorResponse
        {
            public string ServiceName { get; set; }
            public string ServiceVersion { get; set; }
            public string Message { get; set; }
        }

        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;
            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);

            return Task.CompletedTask;
        }
    }
}
