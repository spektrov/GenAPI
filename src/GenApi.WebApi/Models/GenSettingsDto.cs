using System.ComponentModel.DataAnnotations;

namespace GenApi.WebApi.Models;

public class GenSettingsDto
{
    public string SqlScript { get; set; }

    public string Message { get; set; }

    [MinLength(2)]
    required public string AppName { get; set; }

    public int DotnetSdkVersion { get; set; }
}