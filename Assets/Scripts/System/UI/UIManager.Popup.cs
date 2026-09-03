using UnityEngine;

public static partial class UIManager
{
    public static Transform PopupBase;

    public static void OpenPopup(string InName)
    {
        if (PopupBase == null || PopupBase.Find(InName) != null)
            return;

        var prefab = Resources.Load($"Prefab/{InName}");
        if (prefab == null)
        {
            Debug.Log($"{InName} is null");
            return;
        }

        var popup = GameObject.Instantiate(prefab, PopupBase, false) as GameObject;

        UIBases.Push(popup);
    }

    public static void OpenPopup(string InName, Transform InParent)
    {
        if (InParent == null || InParent.Find(InName) != null)
            return;

        var prefab = Resources.Load($"Prefab/{InName}");
        if (prefab == null)
        {
            Debug.Log($"{InName} is null");
            return;
        }

        var popup = GameObject.Instantiate(prefab, InParent, false) as GameObject;

        UIBases.Push(popup);
    }
}
