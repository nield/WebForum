using System.Diagnostics.CodeAnalysis;

namespace WebForum.Application.Common.Settings;

[ExcludeFromCodeCoverage]
public class AppSettings
{
    public bool ApplyMigrations { get; set; }
    public Logs Logs { get; set; }

    public IdentitySettings IdentitySettings { get; set; }
}

[ExcludeFromCodeCoverage]
public class Logs
{
    public Performance Performance { get; set; }
}

[ExcludeFromCodeCoverage]
public class Performance
{
    public bool LogSlowRunningHandlers { get; set; }
    public int SlowRunningHandlerThreshold { get; set; }
}

[ExcludeFromCodeCoverage]
public class IdentitySettings
{
    public string ModeratorPassword { get; set; }
    public string StandardPassword { get; set; }
}