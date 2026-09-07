using UnityEngine;

public class UIBackButton : UIBase
{
    [UIInject("Button_Back")] UIButton Button_Back;

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
        UIBindEvent.BindEvent(Button_Back, OnClickBack);
    }

    public override void UnBindEvent()
    {
        base.UnBindEvent();
        UIBindEvent.UnBindEvent(Button_Back, OnClickBack);
    }

    private void OnClickBack()
    {
        SceneLoadManager.Back();
    }
}
