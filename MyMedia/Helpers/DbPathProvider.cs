using System.IO;

namespace MyMedia.Helpers;

public static class DbPathProvider
{
    public static string GetDbPath() => Path.Combine(AppContext.BaseDirectory, "MyMedia.db");
}
