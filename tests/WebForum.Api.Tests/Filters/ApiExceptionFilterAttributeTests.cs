using FluentValidation.Results;
using WebForum.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using WebForum.Application.Common.Exceptions;
using ValidationException = WebForum.Application.Common.Exceptions.ValidationException;

namespace WebForum.Api.Tests.Filters;

public class ApiExceptionFilterAttributeTests
{
    private readonly ApiExceptionFilterAttribute _filter;
    private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock = new();

    public ApiExceptionFilterAttributeTests()
    {
        _filter = new ApiExceptionFilterAttribute(_webHostEnvironmentMock.Object);
    }

    [Fact]
    public void Given_ValidationExceptionThrown_Then_ReturnBadRequest()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new ValidationException(new ValidationFailure[] { new ValidationFailure("Prop", "Error") })
        };

        _filter.OnException(exceptionContext);

        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void Given_NotFoundException_Then_ReturnNotFound()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new NotFoundException("xxx")
        };

        _filter.OnException(exceptionContext);

        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public void Given_UnknownError_Then_ReturnInternalServer()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new InvalidOperationException()
        };

        _filter.OnException(exceptionContext);

        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<ObjectResult>();
        exceptionContext.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public void Given_UnauthorizedAccessException_Then_ReturnUnauthorized()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new UnauthorizedAccessException("xxx")
        };

        _filter.OnException(exceptionContext);

        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public void Given_ForbiddenAccessException_Then_ReturnForbidden()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new ForbiddenAccessException()
        };

        _filter.OnException(exceptionContext);

        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }

    [Fact]
    public void Given_BadRequestException_Then_ReturnBadRequest()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new BadRequestException("xxx")
        };

        _filter.OnException(exceptionContext);

        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<BadRequestObjectResult>();
    }
}
