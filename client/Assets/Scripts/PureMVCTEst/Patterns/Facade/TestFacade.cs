using PureMVC.Patterns.Facade;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFacade : Facade
{
    public new const string multitonKey = "TestFacade";


    public TestFacade(GameObject canvas):base(multitonKey)
    {
        RegisterCommand(NotificationConstant.LevelUp, () => new TestCommond());
        RegisterMediator(new TestMediator(canvas));
        RegisterProxy(new TestProxy());
    }
}
