using System.Net.Mime;
using AutoMapper;
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
    public async Task<IActionResult> GenerateConsoleApp(
        [FromBody] GenSettingsDto genSettingsDto,
        CancellationToken token)
    {
        try
        {
            var settingsModel = mapper.Map<GenSettingsModel>(genSettingsDto);
            var zipStream = await solutionGenService.GenerateApplicationAsync(settingsModel, token);
            AddResponseHeaders(genSettingsDto.AppName);

            return new FileStreamResult(zipStream, MediaTypeNames.Application.Zip);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    private void AddResponseHeaders(string appName)
    {
        HttpContext.Response.Headers.Append("Content-Disposition", $"attachment; filename={appName}.zip");
        HttpContext.Response.Headers.Append("Content-Type", MediaTypeNames.Application.Zip);
    }
}