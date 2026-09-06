using MyMedia.ViewModels;

namespace MyMedia.Services.Interfaces;

public interface INavigationService
{
    Task NavigateTo(ViewModelBase viewModelBase);
}
