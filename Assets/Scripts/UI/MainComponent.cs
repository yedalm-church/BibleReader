using UnityEngine;
using UnityEngine.UI;

public class MainComponent : UIBase
{
    [UIInject("Button_Reading_Start")] private UIButton Button_Reading_Start;
    [UIInject("Button_History")] private UIButton Button_History;
    [UIInject("Image_Title")] private TimelineLoop TimelineLoop;

    public override void UpdateContent()
    {
        TimelineLoop.Play();
        OnBindEvent();
    }

    public override void OnClose()
    {
        base.OnClose();
        OnUnBindEvent();
    }

    void OnBindEvent()
    {
        UIBindEvent.BindEvent(Button_Reading_Start, OnClickReadingStart);
        UIBindEvent.BindEvent(Button_History, OnClickHistory);
    }

    void OnUnBindEvent()
    {
        UIBindEvent.BindEvent(Button_Reading_Start, OnClickReadingStart);
        UIBindEvent.BindEvent(Button_History, OnClickHistory);
    }

    private void OnClickReadingStart()
    {
        SceneLoadManager.LoadScene("ReadingBible");
    }

    private void OnClickHistory()
    {
        Debug.Log("OnClickHistory");
    }
}
