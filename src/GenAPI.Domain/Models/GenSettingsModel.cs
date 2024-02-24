using GenApi.Domain.Enums;

namespace GenApi.Domain.Models;

public class GenSettingsModel
{
    public DbmsType DbmsType { get; set; }

    public string SqlTableScript { get; set; }

    public string Message { get; set; }

    public string AppName { get; set; }

    public string DotnetSdkVersion { get; set; }
}
