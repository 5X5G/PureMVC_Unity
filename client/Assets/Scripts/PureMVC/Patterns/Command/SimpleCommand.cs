using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Command
{
    public class SimpleCommand : Notifier, ICommand, INotifier
    {        
        public virtual void Execute(INotification notification)
        {
        }
    }
}
