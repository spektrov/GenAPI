using System.Net.Mime;
using AutoMapper;
using GenApi.Domain.Interfaces;
using GenApi.Domain.Models;
using GenApi.WebApi.Filters;
using GenApi.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppGeneratorController(IMapper mapper, ISolutionGenService solutionGenService) : ControllerBase
{
    [ServiceFilter(typeof(AddFileHeaderFilter))]
    [HttpPost]
    public async Task<IActionResult> GenerateConsoleApp(
        [FromBody] GenSettingsDto genSettingsDto,
        CancellationToken token)
    {
        try
        {
            var settingsModel = mapper.Map<GenSettingsModel>(genSettingsDto);
            var zipStream = await solutionGenService.GenerateApplicationAsync(settingsModel, token);

            HttpContext.Items[Constants.ApplicationName] = genSettingsDto.AppName ?? Constants.DefaultName;

            return new FileStreamResult(zipStream, MediaTypeNames.Application.Zip);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}