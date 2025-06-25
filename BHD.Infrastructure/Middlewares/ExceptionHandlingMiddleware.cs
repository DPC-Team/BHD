using BHD.Application.Common.Exceptions;
using BHD.Application.Common.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace BHD.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var errorresponse = new ExceptionMessageResponse
            {
                HasError = true,
            };

            switch (exception)
            {
                case NotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorresponse.Message = ex.Message;
                    break;
                case ArgumentException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorresponse.Message = ex.Message;
                    break;
                case InvalidOperationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorresponse.Message = ex.Message;
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorresponse.Message = ex.Message;
                    break;
                case ValidationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorresponse.Message = ex.Errors.Select(m => m.ErrorMessage);
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorresponse.Message = exception.Message;
                    break;
            }

            _logger.LogError(exception.Message);
            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(errorresponse));
        }
    }
}
