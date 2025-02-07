using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebForum.Application.Common.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class InvalidMappingException: Exception
{
    public InvalidMappingException(string message)
        : base(message)
    {
    }

    public InvalidMappingException(Type fromObject, Type toObject)
        : base($"Cannot map from {fromObject.Name} to {toObject.Name}.")
    {
    }


    protected InvalidMappingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}