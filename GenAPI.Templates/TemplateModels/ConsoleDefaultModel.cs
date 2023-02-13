using GenApi.Templates;
using GenApi.Templates.Common;

namespace GenApi.Templates.TemplateModels;

public class ConsoleDefaultModel : BaseTemplateModel
{
    public override string TemplateName => TemplateNames.ConsoleDefault;

    public string Namespace { get; set; }

    public string Message { get; set; }
}