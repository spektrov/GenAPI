using System.IO.Compression;
using System.Text;
using GenApi.WebApi.Helpers;
using GenApi.WebApi.Models;
using GenApi.WebApi.Models.DTOs;
using GenApi.WebApi.Parsers;
using Microsoft.Build.Locator;

namespace GenApi.WebApi.Services;

public class SolutionGenService(ITemplateParser templateParser) : ISolutionGenService
{
    public async Task<Stream> GenerateApplicationAsync(GenSettingsDto settingsDto, CancellationToken token)
    {
        // Initialize MSBuild for in-memory project creation.
        MSBuildLocator.RegisterDefaults();
        
        var stream = CreateSolutionFile(settingsDto.AppName);

        return await CreateZipArchiveAsync(stream, settingsDto);
    }
    
    public MemoryStream CreateSolutionFile(string appNamespace)
    {
        var slnMemoryStream = new MemoryStream();

        using var writer = new StreamWriter(slnMemoryStream, Encoding.UTF8, leaveOpen: true);

        var slnContent = SolutionGenHelper.GenerateSolutionFileContent(appNamespace);

        writer.Write(slnContent);
        writer.Flush();

        slnMemoryStream.Seek(0, SeekOrigin.Begin);

        return slnMemoryStream;
    }
    
    public async Task<Stream> CreateZipArchiveAsync(Stream slnStream, GenSettingsDto settings)
    {
        // Create a MemoryStream to hold the zip archive.
        var zipMemoryStream = new MemoryStream();

        using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true))
        {
            // Add the .sln stream to the archive.
            var slnEntry = archive.CreateEntry($"{settings.AppName}Solution.sln");
            await using (var entryStream = slnEntry.Open())
            {
                await slnStream.CopyToAsync(entryStream);
            }

            var editorConfigEntry = archive.CreateEntry(".editorconfig");
            await using (var entryStream = editorConfigEntry.Open())
            {
                var editorConfigContent = SolutionGenHelper.GenerateEditorconfigContent();
                var editorConfigBytes = Encoding.UTF8.GetBytes(editorConfigContent);
                await entryStream.WriteAsync(editorConfigBytes, 0, editorConfigBytes.Length);
            }

            // Create a project file (ConsoleApp.csproj) and add it to the archive.
            var projectEntry = archive.CreateEntry($"{settings.AppName}/{settings.AppName}.csproj");
            await using (var entryStream = projectEntry.Open())
            {
                var model = new ConsoleProjectFileDto { SdkVersion = "net8.0" };
                var projectContent = await templateParser.ParseAsync("ConsoleProjectFile", model);
                var projectBytes = Encoding.UTF8.GetBytes(projectContent);
                await entryStream.WriteAsync(projectBytes, 0, projectBytes.Length);
            }

            // Create Program.cs and add it to the archive.
            var programEntry = archive.CreateEntry($"{settings.AppName}/Program.cs");
            await using (var entryStream = programEntry.Open())
            {
                // Generate the Program.cs content here.
                // var programContent = GenerateProgramFileContent();

                var model = new ConsoleDefaultDto { Namespace = settings.AppName, Message = settings.Message };
                var programContent = await templateParser.ParseAsync("ConsoleDefault", model);
                var programBytes = Encoding.UTF8.GetBytes(programContent);
                await entryStream.WriteAsync(programBytes, 0, programBytes.Length);
            }
        }

        // Set the position of the MemoryStream to the beginning.
        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        return zipMemoryStream;
    }
}