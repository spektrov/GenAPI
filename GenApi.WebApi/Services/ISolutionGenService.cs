using GenApi.WebApi.Models;

namespace GenApi.WebApi.Services;

public interface ISolutionGenService
{
    Task<Stream> GenerateApplicationAsync(GenSettingsDto settingsDto, CancellationToken token);
}