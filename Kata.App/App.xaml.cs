using Kata_Login.Navigation;
using Kata_Login.Views;
using Kata.Core.Services;
using Kata.Core.ViewModels;

namespace Kata_Login;

public partial class App : Application
{
    private readonly NavigationService _navigationService;
    private readonly ISettingsService _settingsService;

    public App(NavigationService navigationService, ISettingsService settingsService)
    {
        _navigationService = navigationService;
        _settingsService = settingsService;
        InitializeComponent();

        MainPage = new AppShell();

        navigationService.SetApp(this);
    }

    protected override void OnStart()
    {
        base.OnStart();

        SetStartupPage();
    }

    protected override void OnResume()
    {
        base.OnResume();
        SetStartupPage();
    }

    private void SetStartupPage()
    {
        if (string.IsNullOrWhiteSpace(_settingsService.ServerUrl))
        {
            _navigationService.PushModalAsync<SettingsViewModel>(null);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(_settingsService.Pin))
            {
                _navigationService.PushModalAsync<LoginViewModel>(null);
            }
            else
            {
                _navigationService.PushModalAsync<EnterPinViewModel>(null);
            }
        }
    }
}