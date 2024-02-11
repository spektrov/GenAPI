namespace GenApi.WebApi.Parsers;

public interface ITemplateParser
{
    Task<string> ParseAsync<T>(string templateName, T model);
}