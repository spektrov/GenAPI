using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.Templates.TemplateModels;

namespace GenApi.DomainServices.CommandHandlers;

internal class DomainProjectGenCommand(IFileGenService fileGenService) : IGenCommand
{
    public Task ExecuteAsync(ZipArchive archive, ExtendedGenSettingsModel model, CancellationToken token)
    {
        var fileName = "Domain.csproj";

        return fileGenService.CreateEntryAsync(
            archive,
            fileName.ToDomainProjectFile(model.AppName),
            new ConsoleProjectFileModel { SdkVersion = model.DotnetSdkVersion },
            token);
    }
}
