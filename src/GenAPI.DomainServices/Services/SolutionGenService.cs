using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Services;

public class SolutionGenService(IEnumerable<IGenCommand> commands) : ISolutionGenService
{
    public async Task<Stream> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token)
    {
        var zipMemoryStream = new MemoryStream();

        using var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true);

        foreach (var command in commands)
        {
            await command.ExecuteAsync(archive, settings, token);
        }

        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        return zipMemoryStream;
    }
}