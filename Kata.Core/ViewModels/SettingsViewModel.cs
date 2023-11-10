﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kata.Core.Navigation;
using Kata.Core.Services;

namespace Kata.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel, IInitialize
{
    private readonly ISettingsService _settingsService;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _url = string.Empty;

    public SettingsViewModel(INavigationService navigationService, IMessenger messenger,
        ISettingsService settingsService) : base(navigationService, messenger)
    {
        _settingsService = settingsService;
    }

    public void Init(object parameter)
    {
        Url = _settingsService.ServerUrl ?? string.Empty;
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task Submit()
    {
        _settingsService.ServerUrl = Url;
        await CloseModal(Url);
        await NavigationService.PushModalAsync<LoginViewModel>(null);
    }

    private bool CanSubmit()
    {
        return true;
        // Url.Length > 0;
    }
}