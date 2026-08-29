using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewTestamentComponent : UIBase
{
    [UIInject("Toggle")] private UIToggle NewTestamentToggle;
    [UIInject("ScrollViewList")] private BibleBookList BibleBookList;
    [UIInject("InputField")] private TMP_InputField InputField;
    [UIInject("Button_Input")] private UIButton Button_Input;

    public override void UpdateContent()
    {
        this.transform.SetAsFirstSibling();
        BindEvent();
        BibleBookList.OnClear();
    }

    public override void OnClose()
    {
        UnBindEvent();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(NewTestamentToggle, OnClickNewTestament);
        UIBindEvent.BindEvent(Button_Input, OnClickButtonInput);
    }

    public override void UnBindEvent()
    {
        UIBindEvent.UnBindEvent(NewTestamentToggle, OnClickNewTestament);
        UIBindEvent.UnBindEvent(Button_Input, OnClickButtonInput);
    }

    private void OnClickNewTestament()
    {
        this.transform.SetAsLastSibling();
        BibleBookList.CreateList();
    }

    public void OnClickButtonInput()
    {
        var data = InputField.text;

        if (data == string.Empty)
        {
            BibleBookList.ShowDefaultList();
            return;
        }

        var book = 0;

        if (int.TryParse(data, out int number))
            BibleBookList.Search(number);
        else
            BibleBookList.Search(data);
    }
}
