using System.IO.Compression;

namespace GenApi.Domain.Interfaces;
public interface IArchiveGenService
{
    MemoryStream MemoryStream { get; }

    ZipArchive CreateArchive();

    void ResetPosition();
}
