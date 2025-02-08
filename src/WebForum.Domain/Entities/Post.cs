namespace WebForum.Domain.Entities;

public class Post : BaseAuditableEntity
{
    public required string Title { get; set; }

    public required string Content { get; set; }

    public List<string> Tags { get; set; } = [];

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
    public virtual User User { get; set; }
}