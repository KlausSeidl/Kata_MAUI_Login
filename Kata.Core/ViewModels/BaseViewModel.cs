using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kata.Core.Navigation;

namespace Kata.Core.ViewModels;

public abstract class BaseViewModel : ObservableObject
{
    public BaseViewModel(INavigationService navigationService, IMessenger messenger)
    {
        Messenger = messenger;
        NavigationService = navigationService;
    }

    protected IMessenger Messenger { get; }
    protected INavigationService NavigationService { get; }

    protected Task ShowViewModel<TViewModel>(object parameter = null)
    {
        return NavigationService.PushAsync<TViewModel>(parameter);
    }

    protected Task<ViewModelResultMessage<T>> ShowViewModelWithResult<TViewModel, T>(object parameter = null)
    {
        var taskSource = new TaskCompletionSource<ViewModelResultMessage<T>>();

        Messenger.Register<ViewModelResultMessage<T>>(this, (_, m) =>
        {
            if (m.Sender is TViewModel)
            {
                taskSource.SetResult(m);
                Messenger.Unregister<ViewModelResultMessage<T>>(this);
            }
        });

        var task = taskSource.Task;

        ShowViewModel<TViewModel>(parameter);

        return task;
    }

    protected async Task CloseAndReturn<T>(T result = default)
    {
        await NavigationService.PopAsync();
        Messenger.Send(new ViewModelResultMessage<T>(this, result, false));
    }

    protected async Task CloseModal<T>(T result = default)
    {
        await NavigationService.PopModalAsync();
        Messenger.Send(new ViewModelResultMessage<T>(this, result, false));
    }
    
    protected async Task Cancel<T>()
    {
        await NavigationService.PopAsync();
        Messenger.Send(new ViewModelResultMessage<T>(this, default, true));
    }
}