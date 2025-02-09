using MediatR;
using Microsoft.AspNetCore.Identity;
using WebForum.Domain.Entities;

namespace WebForum.Infrastructure.Tests;

public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected readonly Mock<ILogger<T>> _logger = new();
}

public abstract class BaseTestFixture
{
    protected readonly Mock<IMediator> _mediator = new();
}
