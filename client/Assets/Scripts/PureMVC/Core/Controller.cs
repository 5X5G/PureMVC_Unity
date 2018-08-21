using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Core
{
    public class Controller : IController
    {
        protected IView view;

        protected string multitonKey;

        protected Dictionary<string, Func<ICommand>> commandMap;

        protected static Dictionary<string, IController> instanceMap = new Dictionary<string, IController>();

        /// <summary>Message Constants</summary>
        protected const string MULTITON_MSG = "Controller instance for this Multiton key already constructed!";

        public Controller(string key)
        {
            IController tempController = null;
            if (instanceMap.TryGetValue(key, out tempController) && multitonKey != null) throw new Exception(MULTITON_MSG);
            multitonKey = key;
            if (!instanceMap.ContainsKey(multitonKey))
                instanceMap.Add(multitonKey, this);
            commandMap = new Dictionary<string, Func<ICommand>>();
            InitializeController();
        }

        protected virtual void InitializeController()
        {
            IView tempView = new View(multitonKey);
            view = View.GetInstance(multitonKey, tempView);
        }

        public static IController GetInstance(string key, IController controllerClassRef)
        {
            if (!instanceMap.ContainsKey(key))
                instanceMap.Add(key, controllerClassRef);

            return instanceMap[key];
        }

        public virtual void ExecuteCommand(INotification notification)
        {
            Func<ICommand> commandClassRef = null;
            if (commandMap.TryGetValue(notification.Name, out commandClassRef))
            {
                ICommand commandInstance = commandClassRef();
                commandInstance.InitializeNotifier(multitonKey);
                commandInstance.Execute(notification);
            }
        }

        public virtual void RegisterCommand(string notificationName, Func<ICommand> commandClassRef)
        {
            Func<ICommand> tempCommand = null;
            if (commandMap.TryGetValue(notificationName, out tempCommand) == false)
            {
                view.RegisterObserver(notificationName, new Observer(ExecuteCommand, this));
            }
            commandMap[notificationName] = commandClassRef;
        }

        public virtual void RemoveCommand(string notificationName)
        {
            if (commandMap.ContainsKey(notificationName))
            {
                commandMap.Remove(notificationName);
                view.RemoveObserver(notificationName, this);
            }
        }

        public virtual bool HasCommand(string notificationName)
        {
            return commandMap.ContainsKey(notificationName);
        }

        public static void RemoveController(string key)
        {
            if (instanceMap.ContainsKey(key))
                instanceMap.Remove(key);
        }
    }
}