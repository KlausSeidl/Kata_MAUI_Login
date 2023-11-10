using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kata.Core.Navigation;
using Kata.Core.Services;

namespace Kata.Core.ViewModels;

public partial class LoginViewModel : BaseViewModel, IInitialize
{
    private readonly ISettingsService _settingsService;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _userName = string.Empty;

    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _password = string.Empty;

    public LoginViewModel(INavigationService navigationService, IMessenger messenger,
        ISettingsService settingsService) : base(navigationService, messenger)
    {
        _settingsService = settingsService;
    }

    public void Init(object parameter)
    {
        // Url = _settingsService.ServerUrl ?? string.Empty;
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task Submit()
    {
        // _settingsService.ServerUrl = Url;
        await CloseModal(UserName);
        await NavigationService.PushModalAsync<DefinePinViewModel>(null);
    }

    private bool CanSubmit()
    {
        return string.Equals(UserName, "Hans") && string.Equals(Password, "123");
    }
}