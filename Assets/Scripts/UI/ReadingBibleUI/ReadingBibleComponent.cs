using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ReadingBibleComponent : UIBase
{
    [UIInject("ToggleGroup_Testament")] private ToggleGroup ToggleGroup_Testament;

    [UIInject("Toggle_Old")] private UIToggle Toggle_Old;
    [UIInject("Toggle_New")] private UIToggle Toggle_New;

    [UIInject("OldTestament")] private OldTestamentComponent OldTestament;
    [UIInject("NewTestament")] private NewTestamentComponent NewTestament;

    public override void UpdateContent()
    {
        BindEvent();
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(Toggle_Old, OnClickToggleOld);
        UIBindEvent.BindEvent(Toggle_New, OnClickToggleNew);
    }

    public override void UnBindEvent()
    {
        UIBindEvent.UnBindEvent(Toggle_Old, OnClickToggleOld);
        UIBindEvent.UnBindEvent(Toggle_New, OnClickToggleNew);
    }

    private void OnClickToggleOld()
    {
        NewTestament.HideListView();
    }

    private void OnClickToggleNew()
    {
        OldTestament.HideListView();
    }
}
