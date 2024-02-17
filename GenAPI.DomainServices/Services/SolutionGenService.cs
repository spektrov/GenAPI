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
        var zipMemoryStream = new MemoryStream();

        zipMemoryStream = await CreateZipArchiveAsync(zipMemoryStream, settings, token);

        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        return zipMemoryStream;
    }

    public async Task<MemoryStream> CreateZipArchiveAsync(
        MemoryStream memoryStream, GenSettingsModel settings, CancellationToken token)
    {
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

        await fileGenService.CreateEntryAsync(
            archive,
            $"{settings.AppName}Solution.sln",
            SolutionGenHelper.GenerateSolutionFileContent(settings.AppName),
            token);

        await fileGenService.CreateEntryAsync(
            archive,
            ".editorconfig",
            EditorconfigContent.Value,
            token);

        await fileGenService.CreateEntryAsync(
            archive,
            $"{settings.AppName}/{settings.AppName}.csproj",
            new ConsoleProjectFileModel { SdkVersion = settings.DotnetSdkVersion },
            token);

        await fileGenService.CreateEntryAsync(
            archive,
            $"{settings.AppName}/Program.cs",
            new ConsoleDefaultModel { Namespace = settings.AppName, Message = settings.Message },
            token);

        return memoryStream;
    }
}