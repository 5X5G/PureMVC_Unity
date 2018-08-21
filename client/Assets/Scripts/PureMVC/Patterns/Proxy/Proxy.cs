using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Proxy
{    
    public class Proxy : Notifier, IProxy, INotifier
    {
        public string ProxyName { get; protected set; }

        public object Data { get; set; }

        public static string NAME = "Proxy";

        public Proxy(string proxyName, object data = null)
        {
            ProxyName = proxyName ?? Proxy.NAME;
            if (data != null) Data = data;
        }
      
        public virtual void OnRegister()
        {
        }

        public virtual void OnRemove()
        {
        }                
    }
}
