namespace WebForum.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public long PostId { get; set; }

    public string Content { get; set; }

    public virtual Post Post { get; set; }
}
