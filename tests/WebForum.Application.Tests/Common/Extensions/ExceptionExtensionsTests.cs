﻿namespace WebForum.Application.Tests.Common.Extensions;

public class ExceptionExtensionsTests
{
    [Fact]
    public void When_ExceptionHasMultipleInnerExceptions_Then_ReturnStringWithAllErrors()
    {
        var exception = new Exception("first", new Exception("second"));

        var sut = exception.GetFullErrorMessage();

        sut.Should().Be("first,second");
    }

    [Fact]
    public void When_ExceptionIsNull_Then_ReturnEmptyStringOfErrors()
    {
        Exception exception = null;

        var sut = exception.GetFullErrorMessage();

        sut.Should().BeEmpty();
    }
}
