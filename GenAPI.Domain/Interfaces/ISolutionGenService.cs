using GenApi.Domain.Models;

namespace GenApi.Domain.Interfaces;

public interface ISolutionGenService
{
    Task<Stream> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token);
}