using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebForum.Api.Controllers;

[Route("api/posts")]
[Authorize]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class PostsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public PostsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePostResponse), StatusCodes.Status201Created)]
    public IActionResult CreatePost(CreatePostRequest request, CancellationToken cancellationToken)
    {
        return Ok("new Post");
    }
}
