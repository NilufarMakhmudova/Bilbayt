using System;
using System.Collections.Generic;
using Bilbayt.Core.Exceptions;
using Bilbayt.Infrastructure.ApiExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Bilbayt.Infrastructure.Filters
{
    /// <summary>
    ///     API exception filter.
    ///     For more details on how error result should be returned,
    ///     see https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0#problem-details-for-error-status-codes
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        /// <summary>
        ///     ctor
        /// </summary>
        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ApiModelValidationException), HandleValidationException },
                { typeof(EntityNotFoundException), HandleNotFoundException },
                { typeof(EntityAlreadyExistsException), HandleAlreadyExistsException },
                { typeof(InvalidCredentialsException), HandleInvalidCredentialsException },
            };
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Log.Error(context.Exception, "Handling exception:");

            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            // Model state represents errors that come from two subsystems: model binding and model validation.
            // Errors that originate from model binding are generally data conversion errors.
            // Model validation occurs after model binding and reports errors where data doesn't conform to business rules.
            // Both model binding and model validation occur before the execution of a controller action handler method.
            // Web API controllers don't have to check ModelState.IsValid if they have the [ApiController] attribute.
            // In that case, an automatic HTTP 400 response containing error details is returned when model state is invalid.
            // This is still added here in case we no longer clear the default MVC model validators.
            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            // MS reference: https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0#automatic-http-400-responses
            var exception = context.Exception as ApiModelValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as EntityNotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleAlreadyExistsException(ExceptionContext context)
        {
            var exception = context.Exception as EntityAlreadyExistsException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "The specified resource already exists."
                //Detail = exception.Message
            };

            // This can also be 409 Conflict result:
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.conflictobjectresult?view=aspnetcore-5.0
            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidCredentialsException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidCredentialsException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                Title = "Invalid Username and/or Password.",
                Detail = exception.Message
            };

            context.Result = new UnauthorizedObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
