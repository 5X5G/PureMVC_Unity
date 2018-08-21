using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Core
{
    public class View : IView
    {
        protected string multitonKey;
        
        protected Dictionary<string, IMediator> mediatorMap;
        
        protected Dictionary<string, IList<IObserver>> observerMap;
        
        protected static Dictionary<string, IView> instanceMap = new Dictionary<string, IView>();
        
        protected const string MULTITON_MSG = "View instance for this Multiton key already constructed!";

        public View(string key)
        {
            IView tempView = null;
            if (instanceMap.TryGetValue(key, out tempView) && multitonKey != null) throw new Exception(MULTITON_MSG);
            multitonKey = key;
            if (!instanceMap.ContainsKey(key))
                instanceMap.Add(key, this);
            mediatorMap = new Dictionary<string, IMediator>();
            observerMap = new Dictionary<string, IList<IObserver>>();
            InitializeView();
        }

        protected virtual void InitializeView()
        {
        }

        public static IView GetInstance(string key, IView viewClassRef)
        {
            if (!instanceMap.ContainsKey(key))
                instanceMap.Add(key, viewClassRef);

            return instanceMap[key];
        }

        public virtual void RegisterObserver(string notificationName, IObserver observer)
        {
            IList<IObserver> observers;
            if (observerMap.TryGetValue(notificationName, out observers))
            {
                observers.Add(observer);
            }
            else
            {
                if (!observerMap.ContainsKey(notificationName))
                    observerMap.Add(notificationName, new List<IObserver> { observer });
            }
        }

        public virtual void NotifyObservers(INotification notification)
        {
            IList<IObserver> observers_ref = new List<IObserver>();
            if (observerMap.TryGetValue(notification.Name, out observers_ref))
            {
                // Copy observers from reference array to working array, 
                // since the reference array may change during the notification loop
                foreach (IObserver observer in observers_ref)
                {
                    observer.NotifyObserver(notification);
                }
            }
        }

        public virtual void RemoveObserver(string notificationName, object notifyContext)
        {
            IList<IObserver> observers;
            if (observerMap.TryGetValue(notificationName, out observers))
            {
                for (int i = 0; i < observers.Count; i++)
                {
                    if (observers[i].CompareNotifyContext(notifyContext))
                    {
                        observers.RemoveAt(i);
                        break;
                    }
                }

                // Also, when a Notification's Observer list length falls to
                // zero, delete the notification key from the observer map
                if (observers.Count == 0 && observerMap.ContainsKey(notificationName))
                    observerMap.Remove(notificationName);
            }
        }

        public virtual void RegisterMediator(IMediator mediator)
        {
            if (!mediatorMap.ContainsKey(mediator.MediatorName))
            {
                mediatorMap.Add(mediator.MediatorName, mediator);
                mediator.InitializeNotifier(multitonKey);

                IList<string> interests = mediator.ListNotificationInterests();

                if (interests.Count > 0)
                {
                    IObserver observer = new Observer(mediator.HandleNotification, mediator);
                    for (int i = 0; i < interests.Count; i++)
                    {
                        RegisterObserver(interests[i], observer);
                    }
                }
                // alert the mediator that it has been registered
                mediator.OnRegister();
            }
        }

        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            IMediator mediator = null;
            return mediatorMap.TryGetValue(mediatorName, out mediator) ? mediator : null;
        }

        public virtual IMediator RemoveMediator(string mediatorName)
        {
            IMediator mediator = null;
            if (mediatorMap.ContainsKey(mediatorName))
            {
                mediator = mediatorMap[mediatorName];
                mediatorMap.Remove(mediatorName);

                IList<string> interests = mediator.ListNotificationInterests();
                for (int i = 0; i < interests.Count; i++)
                {
                    RemoveObserver(interests[i], mediator);
                }
                mediator.OnRemove();
            }            
            return mediator;
        }

        public virtual bool HasMediator(string mediatorName)
        {
            return mediatorMap.ContainsKey(mediatorName);
        }

        public static void RemoveView(string key)
        {
            if (instanceMap.ContainsKey(key))
                instanceMap.Remove(key);
        }
    }
}