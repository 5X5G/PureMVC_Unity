namespace PureMVC.Interfaces
{
    public interface IProxy : INotifier
    {
        string ProxyName { get; }

        object Data { set; get; }

        void OnRegister();

        void OnRemove();
    }
}