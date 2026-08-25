using Microsoft.Win32;
using MyMedia.Services.Interfaces;

namespace MyMedia.Services;

public class DialogService : IDialogService
{
    public string? ShowDialog()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Выберите изображение",
            Filter = "Изображения|*.jpg;*.jpeg;*.png;*.webp;*.bmp",
            Multiselect = false,
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}
