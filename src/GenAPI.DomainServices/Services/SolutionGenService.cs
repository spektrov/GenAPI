using System.IO.Compression;
using GenApi.Domain.Constants;
using GenApi.Domain.Extensions;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Mappers;

namespace GenApi.DomainServices.Services;

public class SolutionGenService(IEnumerable<IGenCommand> commands) : ISolutionGenService
{
    public async Task<Stream> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token)
    {
        settings.EntityConfiguration = new DotnetEntityConfigurationModel
        {
            EntityName = settings.TableConfiguration.TableName.ToPascalCase(),
            Properties = settings.TableConfiguration.Columns
            .Select(column => new DotnetPropertyConfigurationModel
            {
                Name = !column.IsPrimaryKey ? column.ColumnName.ToPascalCase() : NameConstants.Id,
                Type = PropertyTypeMapper.Map(settings.DbmsType, column.ColumnType),
                NotNull = column.NotNull,
                IsId = column.IsPrimaryKey,
            }),
        };

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