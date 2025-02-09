using FluentValidation.Results;
using WebForum.Application.Common.Behaviours;
using MediatR;

namespace WebForum.Application.Tests.Common.Behaviours;

public class ValidationBehaviourTests
{
    private readonly ValidationBehaviour<ValidationBehaviourInput, Unit> _validationBehaviour;
    private readonly Mock<RequestHandlerDelegate<Unit>> _pipelineBehaviourDelegateMock = new();
    private readonly Mock<IValidator<ValidationBehaviourInput>> _validatorMock = new();

    public ValidationBehaviourTests()
    {

        _validationBehaviour = new(new[] { _validatorMock.Object });
    }

    [Fact]
    public async Task When_ValidationExists_Then_ReturnTheErrorMessage()
    {

        var failures = new List<ValidationFailure>
        {
            { new ValidationFailure("prop1", "required") }
        };

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<IValidationContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        _pipelineBehaviourDelegateMock.Setup(m => m())
            .ReturnsAsync(() => Unit.Value).Verifiable();

        var sut = await Assert.ThrowsAsync<Application.Common.Exceptions.ValidationException> (() => _validationBehaviour.Handle(new ValidationBehaviourInput(),                                          
                                            _pipelineBehaviourDelegateMock.Object,
                                            CancellationToken.None));

        sut.Errors.Count.Should().Be(1);
    }

    [Fact]
    public async Task When_NoValidationExists_Then_ReturnNoErrorMessage()
    {

        var failures = new List<ValidationFailure>
        {
        };

        _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<IValidationContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        _pipelineBehaviourDelegateMock.Setup(m => m())
            .ReturnsAsync(() => Unit.Value).Verifiable();

        var exception = await Record.ExceptionAsync(() => _validationBehaviour.Handle(new ValidationBehaviourInput(),
                                            _pipelineBehaviourDelegateMock.Object,
                                            CancellationToken.None));
        Assert.Null(exception);

    }
}

public class ValidationBehaviourInput : IRequest<Unit>
{

}

public class ValidationBehaviourHandler : IRequestHandler<ValidationBehaviourInput, Unit>
{
    public Task<Unit> Handle(ValidationBehaviourInput request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}