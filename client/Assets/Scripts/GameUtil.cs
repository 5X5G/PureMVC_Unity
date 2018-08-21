using UnityEngine;
using System.Collections;

public class GameUtil
{
    public static Transform GetChild(GameObject go, string path)
    {
        Transform tran = go.transform.Find(path);
        if (tran == null)
            Debug.Log("Could not find child of "+go.name);
        return tran;
    }

    public static T GetChildComponent<T>(GameObject go, string path) where T : Component
    {
        Transform tran = go.transform.Find(path);
        if (tran == null)
            Debug.Log("Could not find child of " + go.name);
        T t = tran.GetComponent<T>();
        return t;
    }
}
