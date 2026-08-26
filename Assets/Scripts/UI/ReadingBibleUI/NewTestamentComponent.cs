using UnityEngine;
using UnityEngine.UI;

public class NewTestamentComponent : UIBase
{
    [UIInject("Toggle")] private UIToggle NewTestamentToggle;
    [UIInject("ScrollViewList")] private BibleBookList BibleBookList;

    public override void UpdateContent()
    {
        this.transform.SetAsFirstSibling();
        BindEvent();
        BibleBookList.OnClear();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(NewTestamentToggle, OnClickNewTestament);
    }

    private void OnClickNewTestament()
    {
        this.transform.SetAsLastSibling();
        BibleBookList.CreateList();
    }
}
