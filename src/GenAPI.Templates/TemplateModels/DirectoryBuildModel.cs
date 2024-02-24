using GenApi.Templates.Common;

namespace GenApi.Templates.TemplateModels;
public class DirectoryBuildModel : BaseTemplateModel
{
    public override string TemplateName => TemplateNames.DirectoryBuild;

    public string SdkVersion { get; set; }
}
