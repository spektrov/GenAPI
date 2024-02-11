using System.ComponentModel.DataAnnotations;

namespace GenApi.WebApi.Models;

public class GenSettingsDto
{
    public required string Message { get; set; }
    
    [MinLength(2)]
    public required string AppName { get; set; }
}