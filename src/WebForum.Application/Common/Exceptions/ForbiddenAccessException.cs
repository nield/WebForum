using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebForum.Application.Common.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
        : base()
    {
    }

    protected ForbiddenAccessException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
        throw new NotImplementedException();
    }
}
