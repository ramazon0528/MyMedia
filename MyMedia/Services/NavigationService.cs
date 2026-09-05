using System.Windows.Media.Animation;
using MyMedia.Services.Interfaces;
using MyMedia.ViewModels;

namespace MyMedia.Services;

public class NavigationService : ViewModelBase, INavigationService
{
    public ViewModelBase ViewModel { get; set; } = null!;

    public void NavigateTo(ViewModelBase viewModel)
    {
        ViewModel = viewModel;
    }
}
