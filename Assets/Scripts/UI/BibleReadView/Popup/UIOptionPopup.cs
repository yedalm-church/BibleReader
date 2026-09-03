using UnityEngine;

public class UIOptionPopup : UIPopupBase
{
    protected override void Start()
    {
        base.Start();
        BindEvent();
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
        base.OnDestroy();
    }
}
