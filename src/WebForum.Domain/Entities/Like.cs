namespace WebForum.Domain.Entities;

public class Like : BaseAuditableEntity
{
    public long PostId { get; set; }

    public virtual Post Post { get; set; }
}
