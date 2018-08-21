namespace PureMVC.Interfaces
{
    public interface INotifier
    {
        void SendNotification(string notificationName, object body = null, string type = null);

        void InitializeNotifier(string key);
    }
}