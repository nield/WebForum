using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebForum.Application.Common.Exceptions;


[Serializable]
[ExcludeFromCodeCoverage]
public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }

    private BadRequestException()
    : base()
    {
    }

    protected BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
        throw new BadRequestException();
    }
}
