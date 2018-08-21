using System;

namespace PureMVC.Interfaces
{
    public interface IController
    {
        void RegisterCommand(string notificationName, Func<ICommand> commandClassRef);

        void ExecuteCommand(INotification notification);

        void RemoveCommand(string notificationName);

        bool HasCommand(string notificationName);
    }
}