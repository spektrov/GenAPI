using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Locator;

namespace GenApi.WebApi.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class AppGeneratorController : ControllerBase
{
    [HttpGet]
    public IActionResult GenerateConsoleApp([FromQuery] string message)
    {
        try
        {
            // Initialize MSBuild for in-memory project creation.
            MSBuildLocator.RegisterDefaults();

            // Create a MemoryStream to hold the .sln file.
            var slnMemoryStream = CreateSolutionFile();

            // Create a zip archive containing the .sln file.
            Stream zipStream = CreateZipArchive(slnMemoryStream);

            // Set response headers for the zip file.
            Response.Headers.Append("Content-Disposition", "attachment; filename=ConsoleAppSolution.zip");
            Response.Headers.Append("Content-Type", "application/zip");

            // Return the zip archive as a FileStreamResult.
            return new FileStreamResult(zipStream, "application/zip");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
    
    private MemoryStream CreateSolutionFile()
    {
        // Create a MemoryStream to hold the .sln file content.
        var slnMemoryStream = new MemoryStream();

        using var writer = new StreamWriter(slnMemoryStream, Encoding.UTF8, leaveOpen: true);
        // Generate the content of the .sln file.
        var slnContent = GenerateSolutionFileContent();

        // Write the .sln content to the MemoryStream.
        writer.Write(slnContent);
        writer.Flush();

        // Set the position of the MemoryStream to the beginning.
        slnMemoryStream.Seek(0, SeekOrigin.Begin);

        return slnMemoryStream;
    }

    private Stream CreateZipArchive(Stream slnStream)
    {
        // Create a MemoryStream to hold the zip archive.
        var zipMemoryStream = new MemoryStream();

        using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, true))
        {
            // Add the .sln stream to the archive.
            var slnEntry = archive.CreateEntry("ConsoleAppSolution.sln");
            using (var entryStream = slnEntry.Open())
            {
                slnStream.CopyTo(entryStream);
            }

            // Create a project file (ConsoleApp.csproj) and add it to the archive.
            var projectEntry = archive.CreateEntry("ConsoleApp/ConsoleApp.csproj");
            using (var entryStream = projectEntry.Open())
            {
                // Generate the project file content here.
                var projectContent = GenerateProjectFileContent();
                var projectBytes = Encoding.UTF8.GetBytes(projectContent);
                entryStream.Write(projectBytes, 0, projectBytes.Length);
            }

            // Create Program.cs and add it to the archive.
            var programEntry = archive.CreateEntry("ConsoleApp/Program.cs");
            using (var entryStream = programEntry.Open())
            {
                // Generate the Program.cs content here.
                var programContent = GenerateProgramFileContent();
                var programBytes = Encoding.UTF8.GetBytes(programContent);
                entryStream.Write(programBytes, 0, programBytes.Length);
            }
        }

        // Set the position of the MemoryStream to the beginning.
        zipMemoryStream.Seek(0, SeekOrigin.Begin);

        return zipMemoryStream;
    }

    private static string GenerateSolutionFileContent()
    {
        // Generate the content of the .sln file here.
        // You can create an appropriate .sln file content based on your requirements.
        // Include project references and configurations as needed.
        // Example:
        string slnContent = @"
Microsoft Visual Studio Solution File, Format Version 12.00
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""ConsoleApp"", ""ConsoleApp\ConsoleApp.csproj"", ""{YOUR-PROJECT-GUID}""
EndProject
Global
    GlobalSection(SolutionConfigurationPlatforms) = preSolution
        Debug|Any CPU = Debug|Any CPU
        Release|Any CPU = Release|Any CPU
    EndGlobalSection
    GlobalSection(ProjectConfigurationPlatforms) = postSolution
        {YOUR-PROJECT-GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
        {YOUR-PROJECT-GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
        {YOUR-PROJECT-GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {YOUR-PROJECT-GUID}.Release|Any CPU.Build.0 = Release|Any CPU
    EndGlobalSection
    GlobalSection(SolutionProperties) = preSolution
        HideSolutionNode = FALSE
    EndGlobalSection
EndGlobal
";
        return slnContent.Replace("{YOUR-PROJECT-GUID}", Guid.NewGuid().ToString().ToUpper());
    }

    private string GenerateProjectFileContent()
    {
        // Generate the content of ConsoleApp.csproj (the project file) here.
        // You can create an XML string representing the project file with the required settings.
        // Include dependencies, target framework, etc., as needed.
        // Example:
        string projectContent = @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>
</Project>
";
        return projectContent;
    }

    private string GenerateProgramFileContent()
    {
        // Generate the content of Program.cs here.
        // This should include the Console.WriteLine(message) statement.
        // Example:
        string programContent = @"""

                                using System;

                                namespace ConsoleApp
                                {
                                    class Program
                                    {
                                        static void Main(string[] args)
                                        {
                                            string message = ${message};
                                            Console.WriteLine(message);
                                        }
                                    }
                                }

                                """;
        return programContent;
    }
}