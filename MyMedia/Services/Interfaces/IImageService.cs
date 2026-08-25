namespace MyMedia.Services.Interfaces;

public interface IImageService
{
    string CopyImage(string sourceFile);
    string GetImagePath(string fileName);
    void DeleteImage(string fileName);
}
