using TMPro;
using UnityEngine;

public partial class UIBibleReadView
{
    [UIInject("Button_Chapter")] private UIButton Button_Chapter;
    [UIInject("Button_Prev")] private UIButton Button_Prev;
    [UIInject("Button_Next")] private UIButton Button_Next;
    [UIInject("Text_Chapter")] private TMP_Text Text_Chapter;
    [UIInject("Text_Prev")] private TMP_Text Text_Prev;
    [UIInject("Text_Next")] private TMP_Text Text_Next;

    [UIInject("Button_Back")] private UIButton Button_Back;
    [UIInject("Button_Option")] private UIButton Button_Option;

    [UIInject("SwitchActive_Read_Type")] private UISwitchActive SwitchActive_Read_Type;

    private void SetTopUI()
    {
        Text_Chapter.text = $"{BibleManager.Instance.ReadingData.Chapter}¿Â";
        SetPrev_NextButton();
    }

    private void SetPrev_NextButton()
    {
        var chapterCount = TableDataManager.BibleData.GetChapterCount(BibleManager.Instance.ReadingData.Book);
        var currChapter = BibleManager.Instance.ReadingData.Chapter;
        var prevChapter = currChapter - 1;
        var nextChapter = currChapter + 1;

        if (BibleManager.Instance.ReadingData.Chapter == 1)
        {
            Button_Prev.gameObject.SetActive(false);
        }
        else
        {
            Button_Prev.gameObject.SetActive(true);
            Text_Prev.text = $"{prevChapter}¿Â";
        }

        if (chapterCount == BibleManager.Instance.ReadingData.Chapter)
        {
            Button_Next.gameObject.SetActive(false);
        }
        else
        {
            Button_Next.gameObject.SetActive(true);
            Text_Next.text = $"{nextChapter}¿Â";
        }
    }

    private void OnClickChapter()
    {
        var active = !Chapter_ListView.gameObject.activeInHierarchy;
        Chapter_ListView.gameObject.SetActive(active);

        if (active)
        {
            Chapter_ListView.CreateList();
            Chapter_ListView.OnClickChapterItem -= OnClickChapterItem;
            Chapter_ListView.OnClickChapterItem += OnClickChapterItem;
        }
        else
        {
            Chapter_ListView.OnClear();
            Chapter_ListView.OnClickChapterItem -= OnClickChapterItem;
        }
    }

    private void OnClickPrev()
    {
        BibleManager.Instance.ReadingData.Verse = 1;
        BibleManager.Instance.ReadingData.Chapter -= 1;
        BibleManager.Instance.ResetUpdateReadingData();
        UpdateContent();
    }

    private void OnClickNext()
    {
        BibleManager.Instance.ReadingData.Verse = 1;
        BibleManager.Instance.ReadingData.Chapter += 1;
        BibleManager.Instance.ResetUpdateReadingData();
        UpdateContent();
    }

    private void OnClickBack()
    {

    }

    private void OnClickOption()
    {
        UIManager.OpenPopup("Option_Popup");
    }

    private void OnClickReadType()
    {
        var nextType = (int)BibleManager.Instance.ReadingData.ReadType + 1;

        if (BibleManager.Instance.ReadingData.IsValidReadType((ReadType)nextType) == true)
        {
            BibleManager.Instance.ReadingData.SetReadType((ReadType)nextType);
        }
        else
        {
            BibleManager.Instance.ReadingData.SetReadType(ReadType.AlternateReading);
        }

        SwitchActive_Read_Type.Active((int)ReadType);
    }

    private void OnClickChapterItem(int InChapter)
    {
        UpdateContent();

        UIManager.CloseLoading();
    }
}
