using UnityEngine;

public class UITitleBarComponent : UIBase
{
    [UIInject("Button_Back")] private UIButton Button_Back;

    protected override void Start()
    {
        UpdateContent();
    }

    protected override void OnDisable()
    {
        OnClose();
    }

    public override void UpdateContent()
    {
        BindEvent();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(Button_Back, OnClose);
    }

    public override void UnBindEvent()
    {
        UIBindEvent.UnBindEvent(Button_Back, OnClose);
    }

    public override void OnClose()
    {
        base.OnClose();
        BibleManager.Instance.TTS.Stop();
        SceneLoadManager.LoadSceneAsync("ReadingBible").LogExceptionsAndForget();
    }
}
