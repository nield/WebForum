using WebForum.Domain.Common;
using WebForum.Infrastructure.Persistence.Interceptors;

namespace WebForum.Infrastructure.Tests.Persistence.Interceptors;

public class DispatchDomainEventsInterceptorTests : BaseTestFixture
{
    private readonly DispatchDomainEventsInterceptor _interceptor;

    public DispatchDomainEventsInterceptorTests()
    {
        _interceptor = new(_mediator.Object);
    }

    [Fact]
    public async Task Given_EntityHasDomainEvents_When_Saving_Then_DispatchDomainEventsOnEntity()
    {
        var dbContext = new FakeEntityDbContext();

        var entity = new FakeEntity { Name = "fake" };
        var @event = new FakeEvent { Greeting = "Hello World" };

        entity.AddDomainEvent(@event);

        dbContext.FakeEntities.Add(entity);

        await _interceptor.DispatchDomainEvents(dbContext);

        _mediator.Verify(x => x.Publish(It.IsAny<BaseEvent>(), It.IsAny<CancellationToken>()), Times.Once);

        entity.DomainEvents.Should().HaveCount(0);
    }
}