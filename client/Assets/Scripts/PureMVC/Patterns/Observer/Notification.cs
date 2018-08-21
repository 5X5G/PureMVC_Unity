using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
{
    public class Notification : INotification
    {
        public Notification(string name, object body = null, string type = null)
        {
            Name = name;
            Body = body;
            Type = type;
        }

        //注意Name只会在Notification第一次初始化的时候被赋值,慎用set
        public string Name { private set; get; }

        public object Body { set; get; }

        public string Type { set; get; }

        public override string ToString()
        {
            string msg = "Notification Name: " + Name;
            msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
            msg += "\nType:" + ((Type == null) ? "null" : Type.ToString());
            return msg;
        }
    }
}