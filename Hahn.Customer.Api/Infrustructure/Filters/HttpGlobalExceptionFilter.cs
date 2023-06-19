﻿using FluentValidation;
using Hahn.Customers.Api.Infrustructure.ActionReuslts;
using Hahn.Customers.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Hahn.Customers.Api.Infrustructure.Filters;
public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment env;
    private readonly ILogger<HttpGlobalExceptionFilter> logger;

    public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
    {
        this.env = env;
        this.logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        logger.LogError(new EventId(context.Exception.HResult),
            context.Exception,
            context.Exception.Message);

        if (context.Exception.GetType() == typeof(CustomerDomainException))
        {
            HandleCustomerDomainExceptoin(context);
        }
        if (context.Exception.GetType() == typeof(ValidationException))
        {
            // need to handle this part better.
            HandleValidationException(context);
        }
        else
        {
            HandleUnkonwnException(context);
        }
        context.ExceptionHandled = true;
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var problemDetails = new ValidationProblemDetails()
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
        };

        problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

        context.Result = new BadRequestObjectResult(problemDetails);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }

    private void HandleUnkonwnException(ExceptionContext context)
    {
        var json = new JsonErrorResponse
        {
            Messages = new[] { "An error occur.Try it again." }
        };

        if (env.IsDevelopment())
        {
            json.DeveloperMessage = context.Exception;
        }

        context.Result = new InternalServerErrorObjectResult(json);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }

    private static void HandleCustomerDomainExceptoin(ExceptionContext context)
    {
        var problemDetails = new ValidationProblemDetails()
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
        };

        problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

        context.Result = new BadRequestObjectResult(problemDetails);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }

    private class JsonErrorResponse
    {
        public string[] Messages { get; set; }

        public object DeveloperMessage { get; set; }
    }
}

