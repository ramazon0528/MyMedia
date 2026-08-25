using System.IO;
using MyMedia.Services.Interfaces;

namespace MyMedia.Services;

public class ImageService : IImageService
{
    private readonly string _imagesDirectory;

    public ImageService()
    {
        _imagesDirectory = Path.Combine(AppContext.BaseDirectory, "Images");

        Directory.CreateDirectory(_imagesDirectory);
    }

    public string CopyImage(string sourceFile)
    {
        var extension = Path.GetExtension(sourceFile);
        var fileName = $"{Guid.NewGuid()}{extension}";

        var destination = Path.Combine(_imagesDirectory, fileName);

        File.Copy(sourceFile, destination);

        return fileName;
    }

    public void DeleteImage(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return;

        var imagePath = GetImagePath(fileName);

        if (File.Exists(imagePath))
            File.Delete(imagePath);
    }

    public string GetImagePath(string fileName) => Path.Combine(_imagesDirectory, fileName);
}
