using WebForum.Domain.Common;

namespace WebForum.Infrastructure.Tests.Persistence.Interceptors;

public class FakeEvent : BaseEvent
{
    public string Greeting { get; set; } = "";
}
