using UnityEngine;

public partial class UIBibleReadView
{
    [UIInject("Button_Read_Start")] private UIButton Button_Read_Start;
    [UIInject("Button_Read_Pause")] private UIButton Button_Read_Pause;
    [UIInject("Button_Read_Stop")] private UIButton Button_Read_Stop;

    [UIInject("SwitchActive_ReadSelectButtons")] private UISwitchActive SwitchActive_ReadSelectButtons;

    private bool _dirtyPauseReading = false;

    private void OnClickReadStart()
    {
        if (_dirtyPauseReading == false)
        {
            BibleManager.Instance.StartReading
                (BibleManager.Instance.ReadingData.Book,
                 BibleManager.Instance.ReadingData.Chapter);
        }
        else
        {
            _dirtyPauseReading = false;

            var (book, chapter, verse) = BibleManager.Instance.CurrentReadingPosition.Value;

            BibleManager.Instance.StartReading(book, chapter, verse);
        }

        SwitchActive_ReadSelectButtons.Active(0);
    }

    private void OnClickReadPause()
    {
        _dirtyPauseReading = true;
        BibleManager.Instance.StopReading();
        SwitchActive_ReadSelectButtons.Active(1);
    }

    private void OnClickReadStop()
    {
        BibleManager.Instance.ReadingData.ResetVerse();
        BibleManager.Instance.ResetUpdateReadingData();
        BibleManager.Instance.StopReading();
        SwitchActive_ReadSelectButtons.Active(2);
    }
}
