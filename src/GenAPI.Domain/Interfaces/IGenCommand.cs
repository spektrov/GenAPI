using System.IO.Compression;
using GenApi.Domain.Models;

namespace GenApi.Domain.Interfaces;

public interface IGenCommand
{
    Task ExecuteAsync(ZipArchive archive, ExtendedGenSettingsModel model, CancellationToken token);
}
