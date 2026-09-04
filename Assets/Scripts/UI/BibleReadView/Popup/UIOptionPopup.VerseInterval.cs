using UnityEngine;

public partial class UIOptionPopup
{
    [UIInject("SwitchActive_Select_Bg")] private UISwitchActive SwitchActive_Select_Bg;
    [UIInject("SwtichActive_Select_Time")] private UISwitchActive SwtichActive_Select_Time;

    [UIInject("Button_Zero")] private UIButton Button_Zero;
    [UIInject("Button_HalfSecond")] private UIButton Button_HalfSecond;
    [UIInject("Button_1s")] private UIButton Button_1s;
    [UIInject("Button_2s")] private UIButton Button_2s;

    private void OnClickVerseIntervalZero()
    {
        BibleReadingSetting.Instance.VerseInterval = 0;

        SwitchActive_Select_Bg.Active(0);
        SwtichActive_Select_Time.Active(0);
    }

    private void OnClickVerseIntervalHalfSecond()
    {
        BibleReadingSetting.Instance.VerseInterval = 0.5F;

        SwitchActive_Select_Bg.Active(1);
        SwtichActive_Select_Time.Active(1);
    }

    private void OnClickVerseInterval_1s()
    {
        BibleReadingSetting.Instance.VerseInterval = 1F;

        SwitchActive_Select_Bg.Active(2);
        SwtichActive_Select_Time.Active(2);
    }

    private void OnClickVerseInterval_2s()
    {
        BibleReadingSetting.Instance.VerseInterval = 2F;

        SwitchActive_Select_Bg.Active(3);
        SwtichActive_Select_Time.Active(3);
    }
}
