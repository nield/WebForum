
namespace WebForum.Application.Tests;

[Collection("Mapping collection")]
public abstract class BaseTestFixture<T> : BaseTestFixture where T: class
{
    protected readonly Mock<ILogger<T>> _logger = new();


    protected BaseTestFixture(MappingFixture mappingFixture) 
        : base(mappingFixture)
    {

    }

    protected override void DisposeCore()
    {
        base.DisposeCore();

        _logger.VerifyAll();
    }
}

[Collection("Mapping collection")]
public abstract class BaseTestFixture : IDisposable
{
    protected readonly IMapper _mapper;
    protected readonly Mock<IApplicationDbContext> _applicationDbContextMock = new();
    protected readonly Mock<IPostRepository> _postRepositoryMock = new();
    protected readonly Mock<ICommentRepository> _commentRepositoryMock = new();
    protected readonly Mock<ILikeRepository> _likeRepositoryMock = new();
    protected readonly Mock<ICurrentUserService> _currentServiceMock = new();
    protected readonly Mock<IIdentityService> _identityServiceMock = new();

    protected bool _disposedValue;

    protected BaseTestFixture(MappingFixture mappingFixture)
    {
        _mapper = mappingFixture.Mapper;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                DisposeCore();
            }

            _disposedValue = true;
        }
    }

    protected virtual void DisposeCore()
    {
        _postRepositoryMock.VerifyAll();
        _commentRepositoryMock.VerifyAll();
        _likeRepositoryMock.VerifyAll();
        _currentServiceMock.VerifyAll();
        _identityServiceMock.VerifyAll();
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
