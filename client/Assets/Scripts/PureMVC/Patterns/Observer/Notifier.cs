using System;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
{
    public class Notifier : INotifier
    {
        public virtual void SendNotification(string notificationName, object body = null, string type = null)
        {
            Facade.SendNotification(notificationName, body, type);
        }

        public void InitializeNotifier(string key)
        {
            MultitonKey = key;
        }
        protected IFacade Facade
        {
            get
            {
                if (MultitonKey == null) throw new Exception(MultitonKey);
                return Patterns.Facade.Facade.GetInstance(MultitonKey);
            }
        }
        public string MultitonKey { get; protected set; }
    }
}