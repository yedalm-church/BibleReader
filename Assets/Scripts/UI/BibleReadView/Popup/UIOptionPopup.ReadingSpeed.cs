using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public partial class UIOptionPopup
{
    [UIInject("Slider_Speed")] private Slider Slider_Speed;
    [UIInject("Image_Speed_Slider_Select_Bg")] private Image Image_Speed_Slider_Select_Bg;

    private void OnSpeedChanged(float InSpeed)
    {
        var speed = Mathf.Round(InSpeed * 20F) / 20F;

        BibleReadingSetting.Instance.TTSSpeed = speed;

        Image_Speed_Slider_Select_Bg.fillAmount = speed * 0.5F;
    }
}
