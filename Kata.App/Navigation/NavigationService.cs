using System.Diagnostics;
using Kata.Core.Navigation;

namespace Kata_Login.Navigation
{
    public class NavigationService : IFlyoutNavigationService, INavigationService
    {
        public static Dictionary<Type, Type> ViewModelPageMapping = new Dictionary<Type, Type>();

        readonly IServiceProvider _serviceProvider;

        private FlyoutPage _flyoutPage;
        private App _app;

        public NavigationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        protected INavigation Navigation
        {
            get
            {
                if (_flyoutPage != null)
                    return _flyoutPage.Detail.Navigation;

                var navigation = Application.Current?.MainPage?.Navigation;

                if (navigation != null)
                    return navigation;

                else
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();

                    throw new Exception("Navigatoin is null");
                }
            }
        }

        public void SetApp(App app)
        {
            _app = app;
        }

        public void SetFlyoutMainPage<TFlyoutPageViewModel, TMainPageViewModel>(bool showInMenu, object parameter = null)
        {
            if (_app == null)
                throw new Exception("You need to call SetApp before navigating!");

            if (!ViewModelPageMapping.TryGetValue(typeof(TFlyoutPageViewModel), out var flyoutpageType))
                throw new Exception($"{typeof(TFlyoutPageViewModel)} was not registered");

            if (!flyoutpageType.IsSubclassOf(typeof(FlyoutPage)))
                throw new Exception($"{flyoutpageType} is not a Type of FlyoutPage");

            if (!ViewModelPageMapping.TryGetValue(typeof(TMainPageViewModel), out var startPageType))
                throw new Exception($"{typeof(TMainPageViewModel)} for FlyoutMainPage was not registered");

            _flyoutPage = _serviceProvider.GetService(flyoutpageType) as FlyoutPage;
            var flyoutViewModel = _serviceProvider.GetService(typeof(TFlyoutPageViewModel));
            if(flyoutViewModel is IInitialize initialize)
                initialize.Init(parameter);
            _flyoutPage.BindingContext = flyoutViewModel;

            var startPage = _serviceProvider.GetService(startPageType) as Page;
            var viewModel = _serviceProvider.GetService(typeof(TMainPageViewModel));
            if (viewModel is IInitialize initializableVm)
                initializableVm.Init(parameter);
            startPage.BindingContext = viewModel;

            if (showInMenu)
                startPage = new NavigationPage(startPage);

            _flyoutPage.Detail = startPage;
            _app.MainPage = _flyoutPage;
        }

        public Task PushAsync<TViewModel>(object parameter)
        {
            if (_app == null)
                throw new Exception("You need to call SetApp before navigating!");

            if (!ViewModelPageMapping.TryGetValue(typeof(TViewModel), out var navigateToPageType))
                throw new Exception($"{typeof(TViewModel)} was not registered");

            var viewModel = _serviceProvider.GetService(typeof(TViewModel));

            if (viewModel is IInitialize initializableVm)
                initializableVm.Init(parameter);

            var nextPage = _serviceProvider.GetService(navigateToPageType) as Page;
            nextPage.BindingContext = viewModel;

            return Navigation.PushAsync(nextPage);
        }

        public Task PopAsync()
        {
            if (_app == null)
                throw new Exception("You need to call SetApp before navigating!");

            return Navigation.PopAsync();
        }

        public void SetDetailPage<TViewModel>(bool showInMenu, object parameter = null)
            => SetDetailPage(typeof(TViewModel), showInMenu, parameter);


        public void SetDetailPage(Type viewModelType, bool showInMenu, object parameter = null)
        {
            if (_app == null)
                throw new Exception("You need to call SetApp before navigating!");

            if (_flyoutPage == null)
                throw new Exception("No FlyoutPage configured. Call SetFlyoutMainPage for setup.");

            if (!ViewModelPageMapping.TryGetValue(viewModelType, out var detailPageType))
                throw new Exception($"{viewModelType} was not registered");

            var detailPage = _serviceProvider.GetService(detailPageType) as Page;
            
            var viewModel = _serviceProvider.GetService(viewModelType);
            if (viewModel is IInitialize initializableVm)
                initializableVm.Init(parameter);
            detailPage.BindingContext = viewModel;

            if (showInMenu)
                detailPage = new NavigationPage(detailPage);

            _flyoutPage.Detail = detailPage;
        }

        public void SetMainPage<TViewModel>(object parameter, bool wrapInNavigationPage = true)
        {
            _flyoutPage = null;
            
            if (_app == null)
                throw new Exception("You need to call SetApp before navigating!");

            if (!ViewModelPageMapping.TryGetValue(typeof(TViewModel), out var pageType))
                throw new Exception($"{typeof(TViewModel)} was not registered");

            var viewModel = _serviceProvider.GetService(typeof(TViewModel));

            if (viewModel is IInitialize initialize)
                initialize.Init(parameter);
            
            var page = (Page)_serviceProvider.GetService(pageType);

            page.BindingContext = viewModel;

            if(wrapInNavigationPage)
                page = new NavigationPage(page);

            _app.MainPage = page;
        }
    }
}

