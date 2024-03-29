﻿using AutoMapper;
using FluentResults;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;

namespace GenApi.DomainServices.Services;

public class SolutionGenService(
    IMapper mapper,
    IArchiveGenService archiveGenService,
    IEnumerable<IGenCommand> commands)
    : ISolutionGenService
{
    public async Task<Result<Stream>> GenerateApplicationAsync(GenSettingsModel settings, CancellationToken token)
    {
        var model = mapper.Map<ExtendedGenSettingsModel>(settings);
        model.EntityConfiguration = mapper.Map<DotnetEntityConfigurationModel>(model.TableConfiguration);

        using (var archive = archiveGenService.CreateArchive())
        {
            foreach (var command in commands)
            {
                await command.ExecuteAsync(archive, model, token);
            }
        }

        archiveGenService.ResetPosition();

        return archiveGenService.MemoryStream;
    }
}