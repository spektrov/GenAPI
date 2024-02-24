using System.IO.Compression;
using AutoMapper;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.Templates.TemplateModels;

namespace GenApi.DomainServices.CommandHandlers;
internal class DomainEntitiesGenCommand(IFileGenService fileGenService, IMapper mapper) : IGenCommand
{
    public Task ExecuteAsync(ZipArchive archive, ExtendedGenSettingsModel model, CancellationToken token)
    {
        var fileName = $"Entities/{model.EntityConfiguration.EntityName}.cs";

        return fileGenService.CreateEntryAsync(
            archive,
            fileName.ToDomainProjectFile(model.AppName),
            new DomainEntityModel
            {
                Namespace = $"{model.AppName}.Domain.Entities",
                EntityName = $"{model.EntityConfiguration.EntityName}Entity",
                Properties = mapper.Map<IEnumerable<DotnetPropertyModel>>(model.EntityConfiguration.Properties),
            },
            token);
    }
}
