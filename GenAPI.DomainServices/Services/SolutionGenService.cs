using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Helpers;
using GenApi.Templates.StaticTemplates;
using GenApi.Templates.TemplateModels;

namespace GenApi.DomainServices.Services;

public class SolutionGenService(IFileGenService fileGenService) : ISolutionGenService
{
    public async Task<Stream> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token)
    {
        return await CreateZipArchiveAsync(settings, token);
    }

    public async Task<Stream> CreateZipArchiveAsync(GenSettingsModel settings, CancellationToken token)
    {
        // Create a MemoryStream to hold the zip archive.
        var zipMemoryStream = new MemoryStream();

        using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true))
        {
            // Add the .sln stream to the archive.
            await fileGenService.CreateEntryAsync(
                archive,
                $"{settings.AppName}Solution.sln",
                SolutionGenHelper.GenerateSolutionFileContent(settings.AppName),
                token);

            // Add the .editorconfig stream to the archive.
            await fileGenService.CreateEntryAsync(
                archive,
                ".editorconfig",
                EditorconfigContent.Value,
                token);

            // Create a project file .csproj and add it to the archive.
            await fileGenService.CreateEntryAsync(
                archive,
                $"{settings.AppName}/{settings.AppName}.csproj",
                new ConsoleProjectFileModel { SdkVersion = "net8.0" },
                token);

            // Create Program.cs and add it to the archive.
            await fileGenService.CreateEntryAsync(
                archive,
                $"{settings.AppName}/Program.cs",
                new ConsoleDefaultModel { Namespace = settings.AppName, Message = settings.Message },
                token);
        }

        // Set the position of the MemoryStream to the beginning.
        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        return zipMemoryStream;
    }
}