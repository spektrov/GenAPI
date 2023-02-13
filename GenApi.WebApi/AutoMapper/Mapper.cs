using AutoMapper;
using GenApi.Domain.Models;
using GenApi.WebApi.Models;

namespace GenApi.WebApi.AutoMapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<GenSettingsDto, GenSettingsModel>();
    }
}
