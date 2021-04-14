using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaniMusic.Api.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }

            catch (Exception error)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case KeyNotFoundException customException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NullReferenceException customException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error.Message });

                await response.WriteAsync(result);
            }
        }
    }
}
