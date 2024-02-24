using System.ComponentModel.DataAnnotations;
using GenApi.Domain.Enums;

namespace GenApi.WebApi.Models;

public class GenSettingsDto
{
    public string SqlScript { get; set; }

    public DbmsType DbmsType { get; set; }

    [MinLength(2)]
    required public string AppName { get; set; }

    public int DotnetSdkVersion { get; set; }
}