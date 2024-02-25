using AutoMapper;
using FluentResults;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppGeneratorController(IMapper mapper, ISolutionGenService solutionGenService) : ControllerBase
{
    [HttpPost]
    public async Task<Result<Stream>> GenerateConsoleApp(
        [FromBody] GenSettingsDto genSettingsDto,
        CancellationToken token)
    {
        var settingsModel = mapper.Map<GenSettingsModel>(genSettingsDto);
        Response.Headers.Append("Content-Disposition", $"attachment; filename={settingsModel.AppName}.zip");
        return await solutionGenService.GenerateApplicationAsync(settingsModel, token);
    }
}