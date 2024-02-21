using System.Text.RegularExpressions;
using AutoMapper;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Mappers;

public class SqlTableScriptResolver : IValueResolver<GenSettingsModel, ExtendedGenSettingsModel, SqlTableConfigurationModel>
{
    private static readonly string TablePropertyPattern = @"\,\d+|[\)\(;]|\b\d+\b|[\n]";
    private static readonly string createTableSeparator = "create table";
    private static readonly char SemiColonSeparator = ';';
    private static readonly char SpaceSeparator = ' ';
    private static readonly string NotNull = "not null";
    private static readonly string Unique = "unique";
    private static readonly string PrimaryKey = "primary key";

    public SqlTableConfigurationModel Resolve(
        GenSettingsModel source, ExtendedGenSettingsModel destination, SqlTableConfigurationModel destMember, ResolutionContext context)
    {
        // Split script into individual statements
        var statements = Regex.Split(source.SqlTableScript, createTableSeparator, RegexOptions.IgnoreCase);

        // TODO: build multiple sql tables.
        var result = BuildTableConfiguration(statements.FirstOrDefault());
        result.DbmsType = source.DbmsType;

        return result;
    }

    private SqlTableConfigurationModel BuildTableConfiguration(string tableLine)
    {
        string GetTableName() => tableLine.Split(SpaceSeparator, StringSplitOptions.RemoveEmptyEntries).First().Trim();

        return new SqlTableConfigurationModel
        {
            TableName = GetTableName(),
            Columns = Regex.Replace(tableLine, TablePropertyPattern, string.Empty)[(GetTableName().Length + 2)..]
            .Split(SemiColonSeparator, StringSplitOptions.TrimEntries)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(BuildColumnConfiguration),
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
