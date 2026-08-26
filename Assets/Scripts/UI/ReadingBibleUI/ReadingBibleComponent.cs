using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ReadingBibleComponent : UIBase
{
    [UIInject("ToggleGroup_Testament")] private ToggleGroup ToggleGroup_Testament;

    [UIInject("OldTestament")] private OldTestamentComponent OldTestamentComponent;
    [UIInject("NewTestament")] private NewTestamentComponent NewTestamentComponent;

    public override void UpdateContent()
    {
        OnBindEvent();
    }

    void OnBindEvent()
    {
    }
}
