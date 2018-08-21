using System;

namespace PureMVC.Interfaces
{
    public interface IFacade : INotifier
    {
        void RegisterProxy(IProxy proxy);

        IProxy RetrieveProxy(string proxyName);

        IProxy RemoveProxy(string proxyName);

        bool HasProxy(string proxyName);

        void RegisterCommand(string notificationName, Func<ICommand> commandClassRef);

        void RemoveCommand(string notificationName);

        bool HasCommand(string notificationName);

        void RegisterMediator(IMediator mediator);

        IMediator RetrieveMediator(string notificationName);

        IMediator RemoveMediator(string mediatorName);

        bool HasMediator(string mediatorName);

        void NotifyObservers(INotification notification);
    }
}