using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System.Collections.Generic;

namespace PureMVC.Patterns.Mediator
{    
    public class Mediator : Notifier, IMediator, INotifier
    {
        public string MediatorName { get; protected set; }

        public object ViewComponent { get; set; }

        public static string NAME = "Mediator";
        
        public Mediator(string mediatorName, object viewComponent = null)
        {
            MediatorName = mediatorName ?? Mediator.NAME;
            ViewComponent = viewComponent;
        }

        public virtual IList<string> ListNotificationInterests()
        {
            return new List<string>();
        }
        
        public virtual void HandleNotification(INotification notification)
        {
        }

        public virtual void OnRegister()
        {
        }

        public virtual void OnRemove()
        {
        }                
    }
}
