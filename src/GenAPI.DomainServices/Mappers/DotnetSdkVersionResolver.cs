﻿using AutoMapper;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Mappers;

public class DotnetSdkVersionResolver : IValueResolver<GenSettingsModel, ExtendedGenSettingsModel, string>
{
    public string Resolve(GenSettingsModel source, ExtendedGenSettingsModel destination, string member, ResolutionContext context)
    {
        return $"net{source.DotnetSdkVersion}.0";
    }
}
