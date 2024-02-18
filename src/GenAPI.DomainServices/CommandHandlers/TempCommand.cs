using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.Templates.TemplateModels;

namespace GenApi.DomainServices.CommandHandlers;
internal class TempCommand(IFileGenService fileGenService) : IGenCommand
{
    public Task ExecuteAsync(ZipArchive archive, GenSettingsModel model, CancellationToken token)
    {
        var fileName = "Program.cs";

        return fileGenService.CreateEntryAsync(
            archive,
            fileName.ToDomainProjectFile(model.AppName),
            new ConsoleDefaultModel { Namespace = model.AppName, Message = model.Message },
            token);
    }
}
