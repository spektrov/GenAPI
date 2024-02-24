using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.DomainServices.Helpers;

namespace GenApi.DomainServices.CommandHandlers;

internal class SolutionGenCommand(IFileGenService fileGenService) : IGenCommand
{
    public Task ExecuteAsync(ZipArchive archive, ExtendedGenSettingsModel model, CancellationToken token)
    {
        var fileName = $"{model.AppName}Solution.sln";

        return fileGenService.CreateEntryAsync(
           archive,
           fileName.ToCoreSolutionFile(),
           SolutionGenHelper.GenerateSolutionFileContent(model.AppName),
           token);
    }
}
