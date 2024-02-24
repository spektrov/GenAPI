using GenApi.Templates.Common;

namespace GenApi.Templates.TemplateModels;
public class DomainEntityModel : BaseTemplateModel
{
    public override string TemplateName => TemplateNames.DomainEntity;

    public string Namespace { get; set; }

    public string EntityName { get; set; }

    public IEnumerable<DotnetPropertyModel> Properties { get; set; }
}
