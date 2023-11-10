using Kata_Login.Navigation;

namespace Kata_Login
{
    public partial class App : Application
    {
        public App(NavigationService navigationService)
        {
            InitializeComponent();

            MainPage = new AppShell();
            
            navigationService.SetApp(this);
        }
    }
}