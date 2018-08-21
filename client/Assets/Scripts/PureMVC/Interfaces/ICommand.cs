namespace PureMVC.Interfaces
{

    public interface ICommand : INotifier
    {
        void Execute(INotification Notificaton);
    }
}