using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public static partial class UIManager
{
    private static Stack<GameObject> UIBases = new();
    private static Transform UICanvase;

    public static void Initialize()
    {
        PopupBase = GameObject.Find("PopupBase")?.transform;
        UICanvase = GameObject.Find("Canvas")?.transform;
    }

    public static void Open(string InName)
    {
        var prefab = Resources.Load($"Prefab/{InName}");
        if (prefab == null)
        {
            Debug.Log($"{InName} is null");
            return;
        }

        var prefabObject = GameObject.Instantiate(prefab, UICanvase, false) as GameObject;

        UIBases.Push(prefabObject);
    }

    public static void OpenLoading()
    {
        var prefab = Resources.Load($"Prefab/Loading");
        if (prefab == null)
        {
            Debug.Log($"Loading is null");
            return;
        }

        var prefabObject = GameObject.Instantiate(prefab, UICanvase, false) as GameObject;

        UIBases.Push(prefabObject);
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

    public static void CloseLoading()
    {
        Close();
    }
}
