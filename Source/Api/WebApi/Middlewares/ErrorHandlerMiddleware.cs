using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.Wrappers;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

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
                
                var responseModel = new Response(succeeded: false);

                switch (error)
                {
                    case Application.Exceptions.RepositoryException e:
                        
                        // Custom repository error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = e.Message;
                        break;

                    case Application.Exceptions.AppException e:

                        // Custom app error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = e.Message;
                        break;

                    default:
                    
                        // Unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}