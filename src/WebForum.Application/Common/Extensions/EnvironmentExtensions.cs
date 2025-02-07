﻿using WebForum.Application.Common.Constants;

namespace Microsoft.Extensions.Hosting;

public static class EnvironmentExtensions
{
    public static bool IsTest(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.EnvironmentName.Equals(EnvironmentConstants.Test);
    }
}
