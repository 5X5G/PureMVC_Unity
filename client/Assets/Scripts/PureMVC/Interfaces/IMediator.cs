using System.Collections.Generic;

namespace PureMVC.Interfaces
{
    public interface IMediator : INotifier
    {
        string MediatorName { get; }

        object ViewComponent { get; set; }

        IList<string> ListNotificationInterests();

        void HandleNotification(INotification notification);

        void OnRegister();

        void OnRemove();
    }
}