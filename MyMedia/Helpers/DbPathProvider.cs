using System.IO;

namespace MyMedia.Helpers;

public static class DbPathProvider
{
    public static string GetDbPath()
    {
        var appDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MyMedia"
        );

        Directory.CreateDirectory(appDirectory);

        return Path.Combine(appDirectory, "MyMedia.db");
    }
}
