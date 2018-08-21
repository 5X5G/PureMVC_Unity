namespace PureMVC.Interfaces
{
    public interface INotification
    {
        string Name { get; }

        object Body { set; get; }

        string Type { set; get; }

        string ToString(); 
    }
}