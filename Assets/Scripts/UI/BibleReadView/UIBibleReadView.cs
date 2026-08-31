using TMPro;
using UnityEngine;

public class UIBibleReadView : UIBase
{
    [UIInject("Bible_ListView")] private BibleReadList Bible_ListView;
    [UIInject("Button_Chapter")] private UIButton Button_Chapter;
    [UIInject("Text_Read_Type")] private TMP_Text Text_Read_Type;
    [UIInject("Button_Read_Type")] private UIButton Button_Read_Type;
    [UIInject("Button_Read_Start")] private UIButton Button_Read_Start;

    public override void UpdateContent()
    {
        this.transform.SetAsLastSibling();

        var bibleManager = GameObject.FindAnyObjectByType<BibleManager>();
        if (bibleManager == null)
        {
            var bibleObject = new GameObject("BibleManager");
            bibleObject.AddComponent<BibleManager>();
            BibleManager.Instance.SetReadingData(BibleType.Old, 1, 1, 1);
            BibleManager.Instance.SetReadingType(ReadType.AI_Reading);
        }

        Text_Read_Type.text = BibleManager.Instance.GetReadTypeText(BibleManager.Instance.ReadingData.ReadType);

        Bible_ListView.CreateList();
        BindEvent();
    }

    public override void OnClose()
    {
        base.OnClose();
        UnBindEvent();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(Button_Read_Type, OnClickReadType);
        UIBindEvent.BindEvent(Button_Read_Type, OnClickReadType);
        UIBindEvent.BindEvent(Button_Read_Start, OnClickReadStart);
    }

    public override void UnBindEvent()
    {
        UIBindEvent.BindEvent(Button_Chapter, OnClickChapter);
        UIBindEvent.UnBindEvent(Button_Read_Type, OnClickReadType);
        UIBindEvent.BindEvent(Button_Read_Start, OnClickReadStart);
    }

    private void OnClickChapter()
    {

    }

    private void OnClickReadType()
    {

    }

    private void OnClickReadStart()
    {
        BibleManager.Instance.StartReading
            (BibleManager.Instance.ReadingData.Book,
             BibleManager.Instance.ReadingData.Chapter,
             BibleManager.Instance.ReadingData.Verse);
    }
}
