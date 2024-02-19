using AutoMapper;
using GenApi.Domain.Models;
using GenApi.WebApi.Models;

namespace GenApi.WebApi.AutoMapper;

public class SqlTableScriptResolver : IValueResolver<GenSettingsDto, GenSettingsModel, SqlTableConfigurationModel>
{
    private static readonly string[] createTableSeparator = new[] { "CREATE TABLE" };
    private static readonly char[] braceSeparator = new[] { '(', ')' };
    private static readonly char[] propertySeparator = new[] { ',' };
    private static readonly char itemSeparator = ' ';
    private static readonly string Not = "not";
    private static readonly string Null = "null";

    public SqlTableConfigurationModel Resolve(
        GenSettingsDto source, GenSettingsModel destination, SqlTableConfigurationModel destMember, ResolutionContext context)
    {
        var result = new SqlTableConfigurationModel();

        // Split script into individual statements
        var statements = source.SqlScript.Split(createTableSeparator, StringSplitOptions.RemoveEmptyEntries);

        foreach (var statement in statements)
        {
            // Split statement into components
            var components = statement.Split(braceSeparator, StringSplitOptions.RemoveEmptyEntries);

            result.TableName = components[0].Trim();
            var columnLines = components[1].Split(propertySeparator, StringSplitOptions.RemoveEmptyEntries);

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

                // Check for not null constraint if it exists
                column.NotNull = columnComponents.Count > 3
                    && columnComponents[2].Equals(Not, StringComparison.OrdinalIgnoreCase)
                    && columnComponents[3].Equals(Null, StringComparison.OrdinalIgnoreCase);

                columns.Add(column);
            }

            result.Columns = columns;
        }

        return result;
    }
}
