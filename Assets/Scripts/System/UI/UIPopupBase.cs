using UnityEngine;
using UnityEngine.UI;

public class UIPopupBase : UIBase
{
    [UIInject("Button_Background")] private UIButton Button_Background;

    public override void BindEvent()
    {
        base.BindEvent();

        UIBindEvent.BindEvent(Button_Background, OnClickBackground);
    }

    public override void UnBindEvent()
    {
        base.UnBindEvent();

        UIBindEvent.UnBindEvent(Button_Background, OnClickBackground);
    }

    private void OnClickBackground()
    {
        base.OnClose();
    }
}
