using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Wx.Exercises.Api.Exceptions.Models;

namespace Wx.Exercises.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment hostEnvironment)
        {
            try
            {
                if (hostEnvironment.IsDevelopment())
                {
                    await ReadAndLogRequestBody(context, logger);
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                // Fine-grained exception handling can be added here
                // For simplicity I just logged an error and returned a 500
                // We don't want too much information to leak out on exception

                logger.LogError(ex, ex.Message);

                var generalError = new GeneralErrorDetails()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "We encountered an error processing this request. Please check the logs for more details",
                    Detail = ex.Message
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = generalError.Status;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(generalError));
            }
        }

        private static async Task ReadAndLogRequestBody(HttpContext context, ILogger<GlobalExceptionMiddleware> logger)
        {
            var requestBody = await context.Request.GetRequestBodyAsync();

            if (string.IsNullOrEmpty(requestBody))
            {
                logger.LogInformation(requestBody);
            }
        }
    }
}
