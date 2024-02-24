using AutoMapper;
using GenApi.Domain.Constants;
using GenApi.Domain.Extensions;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Mappers;
public class DotnetEntityConfigurationConverter : ITypeConverter<SqlTableConfigurationModel, DotnetEntityConfigurationModel>
{
    public DotnetEntityConfigurationModel Convert(
        SqlTableConfigurationModel source, DotnetEntityConfigurationModel destination, ResolutionContext context)
    {
        return new DotnetEntityConfigurationModel
        {
            EntityName = source.TableName.ToPascalCase(),
            Properties = source.Columns
            .Select(column => new DotnetPropertyConfigurationModel
            {
                Name = !column.IsPrimaryKey ? column.ColumnName.ToPascalCase() : NameConstants.Id,
                Type = PropertyTypeMapper.Map(source.DbmsType, column.ColumnType),
                NotNull = column.NotNull,
                IsId = column.IsPrimaryKey,
            }),
        };
    }
}