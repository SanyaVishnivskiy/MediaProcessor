using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                SetStatusCode(response, error);

                //var result = JsonSerializer.Serialize(new { message = error?.Message });
                var result = JsonSerializer.Serialize(error);//new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }

        private void SetStatusCode(HttpResponse response, Exception error)
        {
            //if (error is Exception)
            //{
            //    response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    return;
            //}

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
