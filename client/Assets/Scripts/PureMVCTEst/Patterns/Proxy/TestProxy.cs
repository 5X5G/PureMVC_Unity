using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Patterns.Proxy;

public class TestProxy:Proxy{

    public new static string NAME = "TestProxy";
    public CharacterInfo Data { set; get; }

    public TestProxy():base(NAME)
    {
        Data = new CharacterInfo();
    }

    public void ChangeLevel(int change)
    {
        Data.Level += change;
        SendNotification(NotificationConstant.LevelChange, Data);
    }
}
