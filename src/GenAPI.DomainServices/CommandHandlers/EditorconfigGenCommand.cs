using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.Templates.StaticTemplates;

namespace GenApi.DomainServices.CommandHandlers;

public class EditorconfigGenCommand(IFileGenService fileGenService) : IGenCommand
{
    public Task ExecuteAsync(ZipArchive archive, GenSettingsModel model, CancellationToken token)
    {
        var fileName = ".editorconfig";

        return fileGenService.CreateEntryAsync(
            archive,
            fileName.ToCoreSolutionFile(model.AppName),
            EditorconfigContent.Value,
            token);
    }
}
