using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BibleItem : UIBase
{
    [UIInject("Text_Number")] private TMP_Text Text_Number;
    [UIInject("Text_Name")] private TMP_Text Text_Name;
    [UIInject("Button_Item")] private UIButton Button_Item;

    private int _bookIndex;
    private string _name;

    public int BookIndex => _bookIndex;
    public string Name => _name;

    protected override void Start()
    {
        BindEvent();
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(Button_Item, OnClickButtonItem);
    }

    public override void UnBindEvent()
    {
        UIBindEvent.UnBindEvent(Button_Item, OnClickButtonItem);
    }

    public void SetData(int InIndex, string InName)
    {
        _bookIndex = InIndex + 1;
        _name = InName;

        Text_Number.text = (InIndex + 1).ToString();
        Text_Name.text = InName;
    }

    private void OnClickButtonItem()
    {
        BibleManager.Instance.StartReading(_bookIndex, 1, 1);
    }
}