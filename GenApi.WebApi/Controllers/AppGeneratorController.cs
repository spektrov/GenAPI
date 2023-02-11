using System.Net.Mime;
using GenApi.WebApi.Models;
using GenApi.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppGeneratorController(ISolutionGenService solutionGenService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> GenerateConsoleApp(
        [FromBody] GenSettingsDto genSettingsDto,
        CancellationToken token)
    {
        try
        {
            var zipStream = await solutionGenService.GenerateApplicationAsync(genSettingsDto, token);
            
            AddHeaders(genSettingsDto.AppName);
            
            return new FileStreamResult(zipStream, MediaTypeNames.Application.Zip);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    private void AddHeaders(string appName)
    {
        Response.Headers.Append("Content-Disposition", $"attachment; filename={appName}.zip");
        Response.Headers.Append("Content-Type", MediaTypeNames.Application.Zip);
    }
}