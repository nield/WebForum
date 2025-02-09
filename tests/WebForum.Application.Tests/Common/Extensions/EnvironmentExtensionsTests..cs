using Microsoft.Extensions.Hosting;
using WebForum.Application.Common.Constants;

namespace WebForum.Application.Tests.Common.Extensions;

public class EnvironmentExtensionsTests
{
    private readonly Mock<IHostEnvironment> _environmentMock = new();

    [Fact]
    public void Given_EnvironmentIsTest_When_CheckingIsTest_Then_IsTrue()
    {
        _environmentMock.SetupGet(x => x.EnvironmentName)
            .Returns(EnvironmentConstants.Test);

        var sut = _environmentMock.Object.IsTest();

        sut.Should().BeTrue();
    }
}
