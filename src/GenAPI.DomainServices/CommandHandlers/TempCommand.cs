﻿using System.IO.Compression;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.DomainServices.Extensions;
using GenApi.Templates.TemplateModels;

namespace GenApi.DomainServices.CommandHandlers;
internal class TempCommand(IFileGenService fileGenService)
{
    public Task ExecuteAsync(ZipArchive archive, ExtendedGenSettingsModel model, CancellationToken token)
    {
        var fileName = "Program.cs";

        return fileGenService.CreateEntryAsync(
            archive,
            fileName.ToDomainProjectFile(model.AppName),
            new ConsoleDefaultModel { Namespace = model.AppName, Message = model.Message },
            token);
    }
}
