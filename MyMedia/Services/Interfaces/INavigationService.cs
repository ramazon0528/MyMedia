using MyMedia.ViewModels;

namespace MyMedia.Services.Interfaces;

public interface INavigationService
{
    void NavigateTo(ViewModelBase viewModelBase);
}
