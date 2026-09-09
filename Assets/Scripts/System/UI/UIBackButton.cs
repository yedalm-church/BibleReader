using System;
using UnityEngine;

public class UIBackButton : UIBase
{
    [UIInject("Button_Back")] UIButton Button_Back;

    public Action OnClickBack;

    protected override void Start()
    {
        BindEvent();
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
    }

    public override void BindEvent()
    {
        base.BindEvent();
        UIBindEvent.BindEvent(Button_Back, OnClickBackButton);
    }

    public override void UnBindEvent()
    {
        base.UnBindEvent();
        UIBindEvent.UnBindEvent(Button_Back, OnClickBackButton);
    }

    private void OnClickBackButton()
    {
        OnClickBack?.Invoke();
        SceneLoadManager.Back();
    }
}
