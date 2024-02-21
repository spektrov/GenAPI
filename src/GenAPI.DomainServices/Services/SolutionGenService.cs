using System.IO.Compression;
using AutoMapper;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Services;

public class SolutionGenService(IMapper mapper, IEnumerable<IGenCommand> commands) : ISolutionGenService
{
    public async Task<Stream> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token)
    {
        var model = mapper.Map<ExtendedGenSettingsModel>(settings);
        model.EntityConfiguration = mapper.Map<DotnetEntityConfigurationModel>(model.TableConfiguration);

        var zipMemoryStream = new MemoryStream();

        using var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true);

        foreach (var command in commands)
        {
            await command.ExecuteAsync(archive, model, token);
        }

        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        return zipMemoryStream;
    }
}