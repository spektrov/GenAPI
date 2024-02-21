using AutoMapper;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Mappers;
public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<GenSettingsModel, ExtendedGenSettingsModel>()
            .ForMember(dest => dest.TableConfiguration, src => src.MapFrom<SqlTableScriptResolver>())
            .ForMember(dest => dest.DotnetSdkVersion, src => src.MapFrom<DotnetSdkVersionResolver>());

        CreateMap<SqlTableConfigurationModel, DotnetEntityConfigurationModel>()
            .ConvertUsing<DotnetEntityConfigurationConverter>();
    }
}
