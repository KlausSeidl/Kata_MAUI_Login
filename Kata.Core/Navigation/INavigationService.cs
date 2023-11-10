namespace Kata.Core.Navigation
{
    public interface INavigationService
    {
        void SetMainPage<TViewModel>(object parameter, bool wrapInNavigationPage = true);
        Task PushAsync<TViewModel>(object parameter);
        Task PopAsync();
    }
}

