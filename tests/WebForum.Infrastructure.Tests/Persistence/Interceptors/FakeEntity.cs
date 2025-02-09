using WebForum.Domain.Common;

namespace WebForum.Infrastructure.Tests.Persistence.Interceptors;

public class FakeEntity : BaseAuditableEntity
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
