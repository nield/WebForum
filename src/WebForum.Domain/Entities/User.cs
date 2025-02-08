using Microsoft.AspNetCore.Identity;

namespace WebForum.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
}
