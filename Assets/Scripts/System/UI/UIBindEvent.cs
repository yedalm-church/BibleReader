using System;
using UnityEngine;
using UnityEngine.UI;

public static class UIBindEvent
{
    public static void BindEvent(UIButton InButton, Action InAction)
    {
        InButton.Bind(InAction);
    }

    public static void UnBindEvent(UIButton InButton, Action InAction)
    {
        InButton.UnBind(InAction);
    }

    public static void BindEvent(UIToggle InToggle, Action InAction)
    {
        InToggle.Bind(InAction);
    }

    public static void UnBindEvent(UIToggle InToggle, Action InAction)
    {
        InToggle.UnBind(InAction);
    }
}
