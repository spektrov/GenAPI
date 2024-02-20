using System.Text.RegularExpressions;
using AutoMapper;
using GenApi.Domain.Enums;
using GenApi.Domain.Models;
using GenApi.WebApi.Models;

namespace GenApi.WebApi.AutoMapper;

public class SqlTableScriptResolver : IValueResolver<GenSettingsDto, GenSettingsModel, SqlTableConfigurationModel>
{
    private static readonly string[] createTableSeparator = new[] { "CREATE TABLE" };
    private static readonly char[] propertySeparator = new[] { ',' };
    private static readonly char itemSeparator = ' ';
    private static readonly string NotNull = "not null";
    private static readonly string Unique = "unique";
    private static readonly string PrimaryKey = "primary key";

    public SqlTableConfigurationModel Resolve(
        GenSettingsDto source, GenSettingsModel destination, SqlTableConfigurationModel destMember, ResolutionContext context)
    {
        var result = new SqlTableConfigurationModel();

        // Split script into individual statements
        var statements = source.SqlScript.Split(createTableSeparator, StringSplitOptions.RemoveEmptyEntries);

        foreach (var statement in statements)
        {
            // Split statement into components
            var components = statement.Split(itemSeparator, StringSplitOptions.RemoveEmptyEntries);

            result.TableName = components[0].Trim();

            var pattern = @"\,\d+|[\)\(;]|\b\d+\b";
            string clearedStatement = Regex.Replace(statement, pattern, string.Empty);
            clearedStatement = Regex.Replace(clearedStatement, result.TableName, string.Empty, RegexOptions.IgnoreCase);

            var columnLines = clearedStatement
                .Split(propertySeparator, StringSplitOptions.TrimEntries)
                .Where(x => !string.IsNullOrEmpty(x));

            var columns = new List<SqlColumnConfigurationModel>();

            foreach (var columnLine in columnLines)
            {
                // Split column line into components and remove leading/trailing white space from each component
                var columnComponents = columnLine.Split(itemSeparator)
                    .Select(component => component.Trim()).ToList();

                // First component should be column name
                var column = new SqlColumnConfigurationModel
                {
                    ColumnName = columnComponents[0]
                };

                // Second component should be column type if it exists
                if (columnComponents.Count > 1)
                {
                    column.ColumnType = columnComponents[1];
                }
                else
                {
                    // TODO: Add error of parsing model.
                    continue;
                }

                column.NotNull = DefineIfNotNull(source.DbmsType, columnLine);

                column.IsPrimaryKey = DefineIfPrimaryKey(source.DbmsType, columnLine);

                columns.Add(column);
            }

            result.Columns = columns;
        }

        return result;
    }

    private bool DefineIfPrimaryKey(DbmsType dbms, string columnDefinition)
    {
        return columnDefinition.Contains(PrimaryKey, StringComparison.OrdinalIgnoreCase);
    }

    private bool DefineIfNotNull(DbmsType dbms, string columnDefinition)
    {
        var isNotNull = columnDefinition.Contains(NotNull, StringComparison.OrdinalIgnoreCase)
            || columnDefinition.Contains(Unique, StringComparison.OrdinalIgnoreCase)
            || DefineIfPrimaryKey(dbms, columnDefinition);

        return isNotNull;
    }
}
