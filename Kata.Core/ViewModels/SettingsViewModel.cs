using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kata.Core.Navigation;

namespace Kata.Core.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _url;
    
    public SettingsViewModel(INavigationService navigationService, IMessenger messenger) : base(navigationService, messenger)
    {
    }
}