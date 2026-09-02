using UnityEngine;

public partial class UIBibleReadView
{
    [UIInject("Button_Read_Start")] private UIButton Button_Read_Start;
    [UIInject("Button_Read_Pause")] private UIButton Button_Read_Pause;
    [UIInject("Button_Read_Stop")] private UIButton Button_Read_Stop;

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
            BibleManager.Instance.StartReading(
                BibleManager.UpdateReadingData.Book,
                BibleManager.UpdateReadingData.Chapter,
                BibleManager.UpdateReadingData.Verse);
        }
    }

    private void OnClickReadPause()
    {
        _dirtyPauseReading = true;
        BibleManager.Instance.StopReading();
    }

    private void OnClickReadStop()
    {
        BibleManager.Instance.ResetReadingVerseData();
        BibleManager.Instance.ResetUpdateReadingData();
        BibleManager.Instance.StopReading();
    }
}
