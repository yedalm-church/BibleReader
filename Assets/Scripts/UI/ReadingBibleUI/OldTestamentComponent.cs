using TMPro;
using UnityEngine;

public class OldTestamentComponent : UIBase
{
    [UIInject("Toggle")] private UIToggle OldTestamentToggle;
    [UIInject("ScrollViewList")] private BibleBookList BibleBookList;
    [UIInject("InputField")] private TMP_InputField InputField;
    [UIInject("Button_Input")] private UIButton Button_Input;

    public override void UpdateContent()
    {
        this.transform.SetAsLastSibling();
        BibleBookList.CreateList();
        BindEvent();
    }

    public override void OnClose()
    {
        base.OnClose();
        UnBindEvent();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(OldTestamentToggle, OnClickOldTestament);
        UIBindEvent.BindEvent(Button_Input, OnClickButtonInput);
    }

    public override void UnBindEvent()
    {
        UIBindEvent.UnBindEvent(OldTestamentToggle, OnClickOldTestament);
        UIBindEvent.UnBindEvent(Button_Input, OnClickButtonInput);
    }

    private void OnClickOldTestament()
    {
        this.transform.SetAsLastSibling();
        BibleBookList.CreateList();
    }

    private void OnClickButtonInput()
    {
        var data = InputField.text;

        if (data == string.Empty)
        {
            BibleBookList.ShowDefaultList();
            return;
        }

        if (int.TryParse(data, out int number))
            BibleBookList.Search(number);
        else
            BibleBookList.Search(data);
    }
}
