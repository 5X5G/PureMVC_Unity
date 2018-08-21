using System;
using System.Collections.Generic;
using PureMVC.Interfaces;

namespace PureMVC.Core
{
    public class Model : IModel
    {
        protected string multitonKey;

        protected readonly Dictionary<string, IProxy> proxyMap;

        protected static readonly Dictionary<string, IModel> instanceMap = new Dictionary<string, IModel>();

        protected const string MULTITON_MSG = "Model instance for this Multiton key already constructed!";

        public Model(string key)
        {
            IModel tempModel = null;
            if (instanceMap.TryGetValue(key,out tempModel) && multitonKey != null) throw new Exception(MULTITON_MSG);
            multitonKey = key;
            instanceMap.Add(key, this);
            proxyMap = new Dictionary<string, IProxy>();
            InitializeModel();
        }

        protected virtual void InitializeModel()
        {
        }

        public static IModel GetInstance(string key, IModel modelClassRef)
        {
            if (!instanceMap.ContainsKey(key))
                instanceMap.Add(key, modelClassRef);

            return instanceMap[key];
        }

        public virtual void RegisterProxy(IProxy proxy)
        {
            proxy.InitializeNotifier(multitonKey);
            proxyMap[proxy.ProxyName] = proxy;
            proxy.OnRegister();
        }

        public virtual IProxy RetrieveProxy(string proxyName)
        {
            IProxy proxy = null;
            return proxyMap.TryGetValue(proxyName, out proxy) ? proxy : null;
        }

        public virtual IProxy RemoveProxy(string proxyName)
        {
            IProxy proxy = null;
            if (proxyMap.ContainsKey(proxyName))
                proxy.OnRemove();

            return proxy;
        }

        public virtual bool HasProxy(string proxyName)
        {
            return proxyMap.ContainsKey(proxyName);
        }

        public static void RemoveModel(string key)
        {
            if (instanceMap.ContainsKey(key))
                instanceMap.Remove(key);
        }
    }
}