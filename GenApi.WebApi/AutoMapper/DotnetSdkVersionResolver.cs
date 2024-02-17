using AutoMapper;
using GenApi.Domain.Models;
using GenApi.WebApi.Models;

namespace GenApi.WebApi.AutoMapper;

public class DotnetSdkVersionResolver : IValueResolver<GenSettingsDto, GenSettingsModel, string>
{
    public string Resolve(GenSettingsDto source, GenSettingsModel destination, string member, ResolutionContext context)
    {
        return source.DotnetSdkVersion switch
        {
            6 => "net6.0",
            7 => "net7.0",
            8 => "net8.0",
            _ => "net8.0",
        };
    }
}
