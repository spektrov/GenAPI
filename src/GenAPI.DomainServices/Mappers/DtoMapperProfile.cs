using AutoMapper;
using GenApi.Domain.Models;
using GenApi.Templates.TemplateModels;

namespace GenApi.DomainServices.Mappers;
public class DtoMapperProfile : Profile
{
    public DtoMapperProfile()
    {
        CreateMap<GenSettingsModel, ExtendedGenSettingsModel>()
            .ForMember(dest => dest.TableConfiguration, src => src.MapFrom<SqlTableScriptResolver>())
            .ForMember(dest => dest.DotnetSdkVersion, src => src.MapFrom<DotnetSdkVersionResolver>());

        CreateMap<SqlTableConfigurationModel, DotnetEntityConfigurationModel>()
            .ConvertUsing<DotnetEntityConfigurationConverter>();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        CreateMap<DotnetPropertyConfigurationModel, DotnetPropertyModel>()
            .ForMember(dest => dest.Nullable, src => src.MapFrom(x => x.NotNull ? string.Empty : "?"));
    }
}
