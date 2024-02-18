namespace GenApi.DomainServices.Extensions;

public static class FileNameExtensions
{
    public static string ToSourceProjectFile(this string filename, string appName)
    {
        return $"src/{appName}/{filename}";
    }

    public static string ToDomainProjectFile(this string filename, string appName)
    {
        return $"src/{appName}.Domain/{filename}";
    }

    public static string ToApiProjectFile(this string filename, string appName)
    {
        return $"src/{appName}.Api/{filename}";
    }

    public static string ToBLLProjectFile(this string filename, string appName)
    {
        return $"src/{appName}.BusinessLogic/{filename}";
    }

    public static string ToDALProjectFile(this string filename, string appName)
    {
        return $"src/{appName}.DataAccess/{filename}";
    }

    public static string ToCoreSolutionFile(this string filename, string appName)
    {
        return $"{appName}/{filename}";
    }
}
