using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterListItem : UIBase
{
    [UIInject("Text_Chapter_Number")] private TMP_Text Text_Chapter_Number;
    [UIInject("Image_Current_Bg")] private Image Image_Current_Bg;
    [UIInject("Image_Current_Check")] private Image Image_Current_Check;
    [UIInject("Button_Chapter")] private UIButton Button_Chapter;

    private int _chapter;

    private Action<int> OnClick;

    protected override void Start()
    {
        BindEvent();
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
    }

    public void SetData(int InChapter, Action<int> InClickItem)
    {
        _chapter = InChapter;
        OnClick = InClickItem;

        Text_Chapter_Number.text = $"{InChapter}¿Â";

        if (InChapter == BibleManager.Instance.ReadingData.Chapter)
        {
            Image_Current_Bg.gameObject.SetActive(true);
            Image_Current_Check.gameObject.SetActive(true);
        }
        else
        {
            Image_Current_Bg.gameObject.SetActive(false);
            Image_Current_Check.gameObject.SetActive(false);
        }
    }

    public override void BindEvent()
    {
        base.BindEvent();
        UIBindEvent.BindEvent(Button_Chapter, OnClickChapter);
    }

    public override void UnBindEvent()
    {
        base.UnBindEvent();
        UIBindEvent.BindEvent(Button_Chapter, OnClickChapter);
    }

    private void OnClickChapter()
    {
        BibleManager.Instance.ReadingData.Verse = 1;
        BibleManager.Instance.ReadingData.Chapter = _chapter;
        BibleManager.Instance.ResetUpdateReadingData();
        OnClick?.Invoke(_chapter);
    }
}
