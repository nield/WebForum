using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebForum.Application.Common.Constants;
using WebForum.Application.Features.Posts.Commands.AddLike;
using WebForum.Application.Features.Posts.Commands.CreateComment;
using WebForum.Application.Features.Posts.Commands.CreatePost;
using WebForum.Application.Features.Posts.Commands.RemoveLike;
using WebForum.Application.Features.Posts.Commands.TagPost;
using WebForum.Application.Features.Posts.Queries.GetCommentsWithPagination;
using WebForum.Application.Features.Posts.Queries.GetPostById;
using WebForum.Application.Features.Posts.Queries.GetPostsWithPagination;

namespace WebForum.Api.Controllers;

[Route("api/posts")]
[Authorize]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public PostsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Used to create a post
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePostResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePost([FromBody]CreatePostRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreatePostCommand>(request);

        var newPostId = await _sender.Send(command, cancellationToken);

        return CreatedAtRoute("GetPostById", 
            new { id = newPostId }, 
            new CreatePostResponse { Id = newPostId });
    }

    /// <summary>
    /// Used to fetch a single post
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetPostById")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetPostByIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPostById(long id, CancellationToken cancellationToken)
    {
        var query = new GetPostByIdQuery { Id = id };

        var data = await _sender.Send(query, cancellationToken);

        var mappedData = _mapper.Map<GetPostByIdResponse>(data);

        return Ok(mappedData);
    }

    /// <summary>
    /// Used to fetch posts with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedListResponse<GetPostsWithPaginationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPostsWithPagination([FromQuery] GetPostsWithPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetPostsWithPaginationQuery>(request);

        var data = await _sender.Send(query, cancellationToken);

        var mappedData = _mapper.Map<PaginatedListResponse<GetPostsWithPaginationResponse>>(data);

        return Ok(mappedData);
    }

    /// <summary>
    /// Used to add comments to a post
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id}/comments")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateComment(long id, [FromBody] CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCommentCommand
        {
            PostId = id,
            Comment = request.Comment,
        };

        var newId = await _sender.Send(command, cancellationToken);

        return Created("", new { Id = newId });
    }

    /// <summary>
    /// Used to fetch comments with pagination
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}/comments")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedListResponse<GetCommentsWithPaginationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCommentsWithPagination(long id, [FromQuery] GetCommentsWithPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetCommentsWithPaginationQuery>(request);
        query.PostId = id;

        var data = await _sender.Send(query, cancellationToken);

        var mappedData = _mapper.Map<PaginatedListResponse<GetCommentsWithPaginationResponse>>(data);

        return Ok(mappedData);
    }

    /// <summary>
    /// Used to like a post
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id}/likes/add")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddLike(long id, CancellationToken cancellationToken)
    {
        var command = new AddLikeCommand
        {
            PostId = id
        };

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Used to remove like
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id}/likes/remove")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveLike(long id, CancellationToken cancellationToken)
    {
        var command = new RemoveLikeCommand
        {
            PostId = id
        };

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Used by moderator users to tag a post
    /// </summary>
    /// <returns></returns>
    [HttpPost("{id}/tags")]
    [Authorize(Roles = RoleConstants.Moderator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> TagPost(long id, [FromBody]TagPostRequest request, CancellationToken cancellationToken)
    {
        var command = new TagPostCommand
        {
            PostId = id,
            Tags = request.Tags
        };

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
}
