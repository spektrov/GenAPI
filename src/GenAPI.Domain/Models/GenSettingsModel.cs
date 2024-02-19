using GenApi.Domain.Enums;

namespace GenApi.Domain.Models;

public class GenSettingsModel
{
    public DbmsType DbmsType { get; set; }

    public SqlTableConfigurationModel TableConfiguration { get; set; }

    public string SqlTableScript { get; set; }

    public DotnetEntityConfigurationModel EntityConfiguration { get; set; }

    public string Message { get; set; }

    public string AppName { get; set; }

    public string DotnetSdkVersion { get; set; }
}
