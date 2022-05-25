using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Middleware
{
    public class ErrorHandingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandingMiddleware> _logger;
        public ErrorHandingMiddleware(ILogger<ErrorHandingMiddleware> logger)
        {
            _logger = logger;
        }

 
   

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
              await  next.Invoke(context);
            }
            catch(BadRequestException badRequestException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {

                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
