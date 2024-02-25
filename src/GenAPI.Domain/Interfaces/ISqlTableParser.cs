using FluentResults;
using GenApi.Domain.Models;

namespace GenApi.Domain.Interfaces;

public interface ISqlTableParser
{
    Result<SqlTableConfigurationModel> BuildTableConfiguration(string tableLine);
}
