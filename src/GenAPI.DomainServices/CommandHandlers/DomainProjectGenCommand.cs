using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.Templates.StaticTemplates;

namespace GenApi.DomainServices.CommandHandlers;

internal class DomainProjectGenCommand(IFileGenService fileGenService) : IGenCommand
{
    public Task ExecuteAsync(ZipArchive archive, ExtendedGenSettingsModel model, CancellationToken token)
    {
        var fileName = $"{model.AppName}.Domain.csproj";

        return fileGenService.CreateEntryAsync(
            archive,
            fileName.ToDomainProjectFile(model.AppName),
            DomainProjectFileContent.Value,
            token);
    }
}
