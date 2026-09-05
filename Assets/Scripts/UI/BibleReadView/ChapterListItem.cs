using System;
using TMPro;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ChapterListItem : UIBase
{
    [UIInject("Text_Chapter_Number")] private TMP_Text Text_Chapter_Number;
    [UIInject("Image_Current_Bg")] private Image Image_Current_Bg;
    [UIInject("Image_Current_Check")] private Image Image_Current_Check;
    [UIInject("Button_Chapter")] private UIButton Button_Chapter;

    private int _chapter;

    private Action<int, int> OnClick;

    public int Chapter => _chapter;

    protected override void Start()
    {
        BindEvent();
    }

    protected override void OnDestroy()
    {
        UnBindEvent();
    }

    public void SetData(int InChapter, Action<int, int> InClickItem)
    {
        _chapter = InChapter;
        OnClick = InClickItem;

        Text_Chapter_Number.text = $"{InChapter}¿Â";

        SetActiveCurrentBg(_chapter == BibleManager.Instance.ReadingData.Chapter);
    }

    public void ResetUI()
    {
        SetActiveCurrentBg(false);
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
        UIManager.OpenLoading();

        var prevChapter = BibleManager.Instance.ReadingData.Chapter;

        BibleManager.Instance.ReadingData.Verse = 1;
        BibleManager.Instance.ReadingData.Chapter = _chapter;
        BibleManager.Instance.ResetUpdateReadingData();

        SetActiveCurrentBg(true);

        OnClick?.Invoke(prevChapter, _chapter);
    }

    private void SetActiveCurrentBg(bool InActive)
    {
        Image_Current_Bg.gameObject.SetActive(InActive);
        Image_Current_Check.gameObject.SetActive(InActive);
    }
}
