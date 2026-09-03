using System.Collections.Generic;
using UnityEngine;

public static partial class UIManager
{
    private static Stack<GameObject> UIBases = new();

    public static void Initialize()
    {
        PopupBase = GameObject.Find("PopupBase").transform;
    }

    public static void Open(UIBase InBase)
    {
        
    }

    public static void Close()
    {
        if (UIBases.Count == 0)
            return;

        var uiObject = UIBases.Pop();
        if (uiObject == null)
            return;

        GameObject.Destroy(uiObject);
    }
}
