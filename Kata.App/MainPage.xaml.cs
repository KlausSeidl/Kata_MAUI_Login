using Kata_Login.Navigation;
using Kata.Core.Navigation;
using Kata.Core.ViewModels;

namespace Kata_Login
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private INavigationService _navigationService;
        
        public MainPage(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void ShowSettings_OnClicked(object sender, EventArgs e)
        {
            // Settings page anzeigen
            await _navigationService.PushAsync<SettingsViewModel>(null);
        }
    }
}