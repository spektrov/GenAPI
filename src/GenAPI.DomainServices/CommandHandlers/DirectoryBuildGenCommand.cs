using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.Templates.TemplateModels;

namespace GenApi.DomainServices.CommandHandlers;
internal class DirectoryBuildGenCommand(IFileGenService fileGenService) : IGenCommand
{
    public Task ExecuteAsync(ZipArchive archive, ExtendedGenSettingsModel model, CancellationToken token)
    {
        var fileName = "Directory.Build.props";

        return fileGenService.CreateEntryAsync(
            archive,
            fileName.ToCoreSolutionFile(),
            new DirectoryBuildModel { SdkVersion = model.DotnetSdkVersion },
            token);
    }
}
