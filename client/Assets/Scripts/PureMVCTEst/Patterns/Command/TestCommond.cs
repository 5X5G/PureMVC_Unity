using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using PureMVC.Patterns.Facade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommond : SimpleCommand
{
    public const string NAME = "TestCommand";

    public override void Execute(INotification notification)
    {
        var testFacade = Facade;
        TestProxy proxy = (TestProxy)testFacade.RetrieveProxy(TestProxy.NAME);
        proxy.ChangeLevel(1);
    }
}
