using FluentResults;
using GenApi.Domain.Models;

namespace GenApi.Domain.Interfaces;

public interface ISolutionGenService
{
    Task<Result<Stream>> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token);
}