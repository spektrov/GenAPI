using System.IO.Compression;
using GenApi.Domain.Extensions;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Mappers;

namespace GenApi.DomainServices.Services;

public class SolutionGenService(IEnumerable<IGenCommand> commands) : ISolutionGenService
{
    public async Task<Stream> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token)
    {
        settings.EntityConfiguration = ParseEntityConfiguration(settings);

        var zipMemoryStream = new MemoryStream();

        using var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true);

        foreach (var command in commands)
        {
            await command.ExecuteAsync(archive, settings, token);
        }

        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        return zipMemoryStream;
    }

    private DotnetEntityConfigurationModel ParseEntityConfiguration(GenSettingsModel settings)
    {
        var response = new DotnetEntityConfigurationModel
        {
            EntityName = settings.TableConfiguration.TableName.ToPascalCase()
        };

        var properties = new List<DotnetPropertyConfigurationModel>();
        foreach (var column in settings.TableConfiguration.Columns)
        {
            properties.Add(new DotnetPropertyConfigurationModel()
            {
                Name = column.ColumnName.ToPascalCase(),
                Type = PropertyTypeMapper.Map(settings.DbmsType, column.ColumnType),
                NotNull = column.NotNull,
            });
        }

        response.Properties = properties;

        return response;
    }
}