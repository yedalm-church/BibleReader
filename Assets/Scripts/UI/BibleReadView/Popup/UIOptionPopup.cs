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

        var interval = BibleReadingSetting.Instance.VerseInterval;

        var index = interval switch
        {
            0F => 0,
            0.5F => 1,
            1F => 2,
            2F => 3,
            _ => 0
        };

        SwitchActive_Select_Bg.Active(index);
        SwtichActive_Select_Time.Active(index);
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

        UIBindEvent.BindEvent(Button_Zero, OnClickVerseIntervalZero);
        UIBindEvent.BindEvent(Button_HalfSecond, OnClickVerseIntervalHalfSecond);
        UIBindEvent.BindEvent(Button_1s, OnClickVerseInterval_1s);
        UIBindEvent.BindEvent(Button_2s, OnClickVerseInterval_2s);
    }

    public override void UnBindEvent()
    {
        base.UnBindEvent();
        UIBindEvent.UnBindEvent(Button_Complete, OnClickComplete);
        Slider_Speed.onValueChanged.RemoveListener(OnSpeedChanged);

        UIBindEvent.UnBindEvent(Button_Zero, OnClickVerseIntervalZero);
        UIBindEvent.UnBindEvent(Button_HalfSecond, OnClickVerseIntervalHalfSecond);
        UIBindEvent.UnBindEvent(Button_1s, OnClickVerseInterval_1s);
        UIBindEvent.UnBindEvent(Button_2s, OnClickVerseInterval_2s);
    }

    private void OnClickComplete()
    {
        base.OnClose();
    }
}

