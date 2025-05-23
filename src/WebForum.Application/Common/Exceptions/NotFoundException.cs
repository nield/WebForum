﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WebForum.Application.Common.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class NotFoundException : Exception
{
    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }

    protected NotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
        throw new NotImplementedException();
    }
}
