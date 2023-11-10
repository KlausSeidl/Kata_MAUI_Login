namespace Kata.Core.ViewModels;

public class ViewModelResultMessage<T>
{
    public ViewModelResultMessage(object sender, T result, bool canceled)
    {
        Sender = sender;
        Result = result;
        Canceled = canceled;
    }

    public object Sender { get; private set; }
    public T Result { get; private set; }
    public bool Canceled { get; private set; }
}

public class NoResult
{
}