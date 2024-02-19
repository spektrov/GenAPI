using AutoMapper;
using GenApi.Domain.Models;
using GenApi.WebApi.Models;

namespace GenApi.WebApi.AutoMapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<GenSettingsDto, GenSettingsModel>()
            .ForMember(dest => dest.SqlTableScript, src => src.MapFrom(x => x.SqlScript))
            .ForMember(dest => dest.TableConfiguration, src => src.MapFrom<SqlTableScriptResolver>())
            .ForMember(dest => dest.DotnetSdkVersion, src => src.MapFrom<DotnetSdkVersionResolver>());
    }
}
