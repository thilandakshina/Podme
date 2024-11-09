﻿using Podme.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Podme.API
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

        public async Task InvokeAsync(HttpContext context)
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
            response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            switch (exception)
            {
                case SubscriptionException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new { Message = exception.Message, StatusCode = response.StatusCode };
                    break;
                default:
                    _logger.LogError(exception, "An unexpected error occurred");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse = new { Message = "An unexpected error occurred", StatusCode = response.StatusCode };
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}
