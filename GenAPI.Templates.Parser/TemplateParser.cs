using RazorEngineCore;

namespace GenAPI.Templates.Parser;

public class TemplateParser : ITemplateParser
{
    public async Task<string> ParseAsync<T>(string templateName, T model)
    {
        var templatePath = GetTemplatePath(templateName);

        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException($"Template not found at '{templatePath}'.");
        }

        var templateContent = await File.ReadAllTextAsync(templatePath);

        var razorEngine = new RazorEngine();
        var template = await razorEngine.CompileAsync(templateContent);
        var parsedContent = await template.RunAsync(model);

        return parsedContent;
    }

    private static string GetTemplatePath(string templateName)
    {
        var templatesDirectory = @"Models\Templates";
        var templateFile = $"{templateName}.cshtml";

        return Path.Combine(templatesDirectory, templateFile);
    }
}