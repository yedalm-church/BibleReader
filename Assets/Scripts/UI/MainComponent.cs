using UnityEngine;
using UnityEngine.UI;

public class MainComponent : UIBase
{
    [UIInject("Button_Reading_Start")] private UIButton Button_Reading_Start;
    [UIInject("Button_History")] private UIButton Button_History;

    public override void UpdateContent()
    {
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
        SceneLoadManager.LoadSceneAsync("ReadingBible").LogExceptionsAndForget();
    }

    private void OnClickHistory()
    {
        Debug.Log("OnClickHistory");
    }
}
