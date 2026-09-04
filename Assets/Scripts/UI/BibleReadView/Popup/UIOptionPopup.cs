using UnityEngine;
using UnityEngine.Scripting;

public partial class UIOptionPopup : UIPopupBase
{
    [UIInject("Button_Complete")] private UIButton Button_Complete;

    protected override void Start()
    {
        UpdateContent();
        BindEvent();
    }

    public override void UpdateContent()
    {
        base.UpdateContent();

        var speed = BibleReadingSetting.Instance.TTSSpeed;

        Image_Speed_Slider_Select_Bg.fillAmount = speed * 0.5F;
        Slider_Speed.value = speed;
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
        base.OnDestroy();
    }

    public override void BindEvent()
    {
        base.BindEvent();

        UIBindEvent.BindEvent(Button_Complete, OnClickComplete);
        Slider_Speed.onValueChanged.AddListener(OnSpeedChanged);
    }

    public override void UnBindEvent()
    {
        base.UnBindEvent();
        UIBindEvent.UnBindEvent(Button_Complete, OnClickComplete);
        Slider_Speed.onValueChanged.RemoveListener(OnSpeedChanged);
    }

    private void OnClickComplete()
    {
        base.OnClose();
    }
}

