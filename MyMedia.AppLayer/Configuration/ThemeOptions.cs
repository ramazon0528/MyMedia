namespace MyMedia.AppLayer.Configuration;

public class ThemeOptions
{
    public string Current { get; set; } = string.Empty;
    public Dictionary<string, ThemeDefinition> Themes { get; set; } = [];
}

public class ThemeDefinition
{
    public string Source { get; set; } = string.Empty;
}
