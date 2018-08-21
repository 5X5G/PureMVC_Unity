using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Core;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Facade
{
    public class Facade : IFacade
    {
        protected IController controller;
        
        protected IModel model;
        
        protected IView view;
        
        protected string multitonKey;

        protected static Dictionary<string, IFacade> instanceMap = new Dictionary<string, IFacade>();

        protected const string MULTITON_MSG = "View instance for this Multiton key already constructed!";

        public Facade(string key)
        {
            if (instanceMap.ContainsKey(key))
                throw new Exception(MULTITON_MSG);
            InitializeNotifier(key);
            if (!instanceMap.ContainsKey(key))
                instanceMap.Add(key, this);
            InitializeFacade();
        }

        protected virtual void InitializeFacade()
        {
            InitializeModel();
            InitializeController();
            InitializeView();
        }

        public static IFacade GetInstance(string key)
        {
            if (instanceMap.ContainsKey(key))
                return instanceMap[key];
            else
                return null;
        }

        protected virtual void InitializeModel()
        {
            var tempModel = new Model(multitonKey);
            model = Model.GetInstance(multitonKey, tempModel);
        }

        protected virtual void InitializeView()
        {
            var tempView = new View(multitonKey);
            view = View.GetInstance(multitonKey, tempView);
        }

        protected virtual void InitializeController()
        {
            var tempController = new Controller(multitonKey);
            controller = Controller.GetInstance(multitonKey, tempController);
        }

        public virtual void RegisterCommand(string notificationName, Func<ICommand> commandClassRef)
        {
            controller.RegisterCommand(notificationName, commandClassRef);
        }

        public virtual void RemoveCommand(string notificationName)
        {
            controller.RemoveCommand(notificationName);
        }

        public virtual bool HasCommand(string notificationName)
        {
            return controller.HasCommand(notificationName);
        }

        public virtual void RegisterProxy(IProxy proxy)
        {
            model.RegisterProxy(proxy);
        }

        public virtual IProxy RetrieveProxy(string proxyName)
        {
            return model.RetrieveProxy(proxyName);
        }

        public virtual IProxy RemoveProxy(string proxyName)
        {
            return model.RemoveProxy(proxyName);
        }

        public virtual bool HasProxy(string proxyName)
        {
            return model.HasProxy(proxyName);
        }

        public virtual void RegisterMediator(IMediator mediator)
        {
            view.RegisterMediator(mediator);
        }

        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            return view.RetrieveMediator(mediatorName);
        }

        public virtual IMediator RemoveMediator(string mediatorName)
        {
            return view.RemoveMediator(mediatorName);
        }

        public virtual bool HasMediator(string mediatorName)
        {
            return view.HasMediator(mediatorName);
        }

        public virtual void SendNotification(string notificationName, object body = null, string type = null)
        {
            NotifyObservers(new Notification(notificationName, body, type));
        }

        public virtual void NotifyObservers(INotification notification)
        {
            view.NotifyObservers(notification);
        }

        public virtual void InitializeNotifier(string key)
        {
            multitonKey = key;
        }

        public static bool HasCore(string key)
        {
            IFacade tempFacade = null;
            return instanceMap.TryGetValue(key, out tempFacade);
        }

        public static void RemoveCore(string key)
        {
            IFacade tempFacade = null;
            if (instanceMap.TryGetValue(key, out tempFacade) == false) return;
            Model.RemoveModel(key);
            View.RemoveView(key);
            Controller.RemoveController(key);
            if (instanceMap.ContainsKey(key))
                instanceMap.Remove(key);
        }
    }
}