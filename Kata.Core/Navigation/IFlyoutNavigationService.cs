namespace Kata.Core.Navigation;

public interface IFlyoutNavigationService
{
    void SetFlyoutMainPage<TFlyoutPageViewModel, TMainPageViewModel>(bool showInMenu, object parameter = null);
    void SetDetailPage<TViewModel>(bool showInMenu, object parameter = null);
    void SetDetailPage(Type viewModelType, bool showInMenu, object parameter = null);
}