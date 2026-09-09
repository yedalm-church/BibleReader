using UnityEngine;

public partial class UIBibleReadView
{
    [UIInject("Read")] private GameObject Read;
    [UIInject("Button_Read_Start")] private UIButton Button_Read_Start;
    [UIInject("Button_Read_Pause")] private UIButton Button_Read_Pause;
    [UIInject("Button_Read_Stop")] private UIButton Button_Read_Stop;

    [UIInject("SwitchActive_ReadSelectButtons")] private UISwitchActive SwitchActive_ReadSelectButtons;
    [UIInject("SwitchActive_ReadButtons")] private UISwitchActive SwitchActive_ReadButtons;

    [UIInject("Button_AlternateReading_Start")] private UIButton Button_AlternateReading_Start;
    [UIInject("Button_AlternateReading_Pause")] private UIButton Button_AlternateReading_Pause;
    [UIInject("Button_AlternateReading_Stop")] private UIButton Button_AlternateReading_Stop;

    private bool _dirtyPauseReading = false;

    private bool _dirtyPauseAlternateReading = false;

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
        _dirtyPauseReading = true;
        BibleManager.Instance.ReadingData.ResetVerse();
        BibleManager.Instance.ResetUpdateReadingData();
        BibleManager.Instance.StopReading();
        SwitchActive_ReadSelectButtons.Active(2);
    }

    private void SetActiveReadPlay(bool InActive)
    {
        Read.SetActive(InActive);
    }

    private void SetReadButtonType()
    {
        switch (ReadType)
        {
            case ReadType.AlternateReading:
                SwitchActive_ReadButtons.Active(1);
                break;
            case ReadType.AI_Reading:
                SwitchActive_ReadButtons.Active(0);
                break;
            default:
                SwitchActive_ReadButtons.HideAll();
                break;
        }
    }

    private void OnClickAlternateReadingStart()
    {
        if (_dirtyPauseAlternateReading == false)
        {
            BibleManager.Instance.StartAlternateReading
                (BibleManager.Instance.ReadingData.Book,
                 BibleManager.Instance.ReadingData.Chapter,
                 BibleManager.Instance.ReadingData.Verse);
        }
        else
        {
            _dirtyPauseAlternateReading = false;

            var (book, chapter, verse) = BibleManager.Instance.CurrentReadingPosition.Value;

            BibleManager.Instance.StartReading(book, chapter, verse);
        }

        SwitchActive_ReadSelectButtons.Active(0);
    }

    private void OnClickAlternateReadingPause()
    {
        _dirtyPauseAlternateReading = true;
        BibleManager.Instance.StopReading();
        SwitchActive_ReadSelectButtons.Active(1);
    }

    private void OnClickAlternateReadingStop()
    {
        _dirtyPauseAlternateReading = false;
        BibleManager.Instance.ReadingData.ResetVerse();
        BibleManager.Instance.ResetUpdateReadingData();
        BibleManager.Instance.StopReading();
        SwitchActive_ReadSelectButtons.Active(2);
    }
}
