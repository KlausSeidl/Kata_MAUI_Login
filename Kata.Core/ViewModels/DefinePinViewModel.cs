using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kata.Core.Navigation;
using Kata.Core.Services;

namespace Kata.Core.ViewModels;

public partial class DefinePinViewModel : BaseViewModel, IInitialize
{
    private readonly ISettingsService _settingsService;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _pIN = string.Empty;

    public DefinePinViewModel(INavigationService navigationService, IMessenger messenger,
        ISettingsService settingsService) : base(navigationService, messenger)
    {
        _settingsService = settingsService;
    }

    public void Init(object parameter)
    {
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task Submit()
    {
        _settingsService.Pin = PIN;
        await CloseModal(PIN);
    }

    private bool CanSubmit()
    {
        return true;
    }
}