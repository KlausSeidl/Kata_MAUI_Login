namespace Kata_Login.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}