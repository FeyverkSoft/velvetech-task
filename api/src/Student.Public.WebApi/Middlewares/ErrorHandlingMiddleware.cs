﻿using System;
using System.Net;
using System.Threading.Tasks;
using Student.Public.WebApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Student.Public.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();
        private static readonly RouteData EmptyRouteData = new RouteData();

        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private readonly ILoggerFactory _loggerFactory;

        public ErrorHandlingMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor, ILoggerFactory loggerFactory)
        {
            _next = next;
            _executor = executor;
            _loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException apiException)
            {
                var logger = _loggerFactory.CreateLogger("System");
                logger.LogError(apiException, apiException.Message);
                var actionContext = new ActionContext(context, context.GetRouteData() ?? EmptyRouteData, EmptyActionDescriptor);
                var resp = new ProblemDetails
                {
                    Type = apiException.Code ?? ErrorCodes.InternalServerError,
                    Status = apiException.GetHttpStatusCode(),
                    Detail = apiException.Message,
                };
                resp.Extensions.Add("traceId", context.TraceIdentifier);

                await _executor.ExecuteAsync(actionContext, new ObjectResult(resp)
                {
                    StatusCode = apiException.GetHttpStatusCode()
                });
            }
            catch (Exception exc)
            {
                var logger = _loggerFactory.CreateLogger("System");
                logger.LogError(exc, exc.Message);
                var actionContext = new ActionContext(context, context.GetRouteData() ?? EmptyRouteData, EmptyActionDescriptor);
                var resp = new ProblemDetails
                {
                    Type = ErrorCodes.InternalServerError,
                    Status = (Int32)HttpStatusCode.InternalServerError,
#if DEBUG
                    Detail = exc.Message,
#else
                    Detail = "internal server error"
#endif
                };
                resp.Extensions.Add("traceId", context.TraceIdentifier);

                await _executor.ExecuteAsync(actionContext, new ObjectResult(resp)
                {
                    StatusCode = (Int32)HttpStatusCode.InternalServerError
                });
            }
        }
    }
}
