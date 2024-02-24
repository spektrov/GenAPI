using System.Text.RegularExpressions;
using AutoMapper;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Mappers;

public class SqlTableScriptResolver : IValueResolver<GenSettingsModel, ExtendedGenSettingsModel, SqlTableConfigurationModel>
{
    private static readonly string TablePropertyPattern = @"\,\d+|[\)\(;]|\b\d+\b|[\n]";
    private static readonly string createTableSeparator = "create table";
    private static readonly char SemiColonSeparator = ';';
    private static readonly char ComaSeparator = ',';
    private static readonly char SpaceSeparator = ' ';
    private static readonly string NotNull = "not null";
    private static readonly string Unique = "unique";
    private static readonly string PrimaryKey = "primary key";

    public SqlTableConfigurationModel Resolve(
        GenSettingsModel source, ExtendedGenSettingsModel destination, SqlTableConfigurationModel destMember, ResolutionContext context)
    {
        // Split script into individual statements
        source.SqlTableScript = source.SqlTableScript.Replace("\n", string.Empty);

        var statements = Regex.Split(source.SqlTableScript, createTableSeparator, RegexOptions.IgnoreCase)
            .Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim());

        // TODO: build multiple sql tables.
        var result = BuildTableConfiguration(statements.FirstOrDefault());
        result.DbmsType = source.DbmsType;

        return result;
    }

    private SqlTableConfigurationModel BuildTableConfiguration(string tableLine)
    {
        var tableName = tableLine.Split(SpaceSeparator, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim();
        var allItems = Regex.Replace(tableLine, TablePropertyPattern, string.Empty)[tableName.Length..];

        var items = allItems.Split(ComaSeparator, StringSplitOptions.TrimEntries);
        var aItems = items.Where(x => !string.IsNullOrWhiteSpace(x));
        var columns = aItems.Select(BuildColumnConfiguration);

        return new SqlTableConfigurationModel
        {
            TableName = tableName,
            Columns = columns,
        };
    }

    private SqlColumnConfigurationModel BuildColumnConfiguration(string columnLine)
    {
        // Split column line into components and remove leading/trailing white space from each component
        var columnComponents = columnLine.Split(SpaceSeparator)
            .Select(component => component.Trim()).ToList();

        if (columnComponents.Count < 2)
        {
            // TODO: Add error of parsing model.
            return null;
        }

        var column = new SqlColumnConfigurationModel
        {
            ColumnName = columnComponents[0], // First component should be column name
            ColumnType = columnComponents[1], // Second component should be column type
            NotNull = DefineIfNotNull(columnLine),
            IsPrimaryKey = DefineIfPrimaryKey(columnLine),
        };

        return column;
    }

    private bool DefineIfPrimaryKey(string columnDefinition)
    {
        return columnDefinition.Contains(PrimaryKey, StringComparison.OrdinalIgnoreCase);
    }

    private bool DefineIfNotNull(string columnDefinition)
    {
        var isNotNull = columnDefinition.Contains(NotNull, StringComparison.OrdinalIgnoreCase)
            || columnDefinition.Contains(Unique, StringComparison.OrdinalIgnoreCase)
            || DefineIfPrimaryKey(columnDefinition);

        return isNotNull;
    }
}
