using System.Windows.Media.Animation;
using MyMedia.Services.Interfaces;
using MyMedia.ViewModels;

namespace MyMedia.Services;

public class NavigationService : ViewModelBase, INavigationService
{
    public ViewModelBase ViewModel { get; set; } = null!;
    public double Opacity { get; set; } = 1;

    public async Task NavigateTo(ViewModelBase viewModel)
    {
        await Task.Factory.StartNew(() =>
        {
            for (double i = 1.0; i >= 0; i -= 0.1)
            {
                Opacity = i;
                Thread.Sleep(20);
            }
        });

        ViewModel = viewModel;

        await Task.Factory.StartNew(() =>
        {
            for (double i = 0; i <= 1.0; i += 0.1)
            {
                Opacity = i;
                Thread.Sleep(20);
            }
        });
    }
}
