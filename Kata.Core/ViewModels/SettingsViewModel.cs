using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kata.Core.Navigation;
using Kata.Core.Services;

namespace Kata.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel, IInitialize
{
    private ISettingsService _settingsService;
    
    public SettingsViewModel(INavigationService navigationService, IMessenger messenger, ISettingsService settingsService)  :  base(navigationService, messenger)
    {
        _settingsService = settingsService;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _url = string.Empty;
    
    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private Task Submit()
    {
        _settingsService.ServerUrl = Url;
        return CloseAndReturn(Url);
    }

    private bool CanSubmit() => true; // Url.Length > 0;

    public void Init(object parameter)
    {
        Url = _settingsService.ServerUrl ?? string.Empty;
    }
}