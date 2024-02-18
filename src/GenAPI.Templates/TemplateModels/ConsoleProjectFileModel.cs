using GenApi.Templates;
using GenApi.Templates.Common;

namespace GenApi.Templates.TemplateModels;

public class ConsoleProjectFileModel : BaseTemplateModel
{
    public override string TemplateName => TemplateNames.ConsoleProjectFile;

    public string SdkVersion { get; set; }
}