namespace GenAPI.Templates.Parser
{
    public interface ITemplateParser
    {
        Task<string> ParseAsync<T>(string templateName, T model);
    }
}
