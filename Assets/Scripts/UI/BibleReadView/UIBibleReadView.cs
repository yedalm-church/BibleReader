using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class UIBibleReadView : UIBase
{
    [UIInject("Bible_ListView")] private BibleReadList BibleReadList;
    [UIInject("Chapter_ListView")] private UIChapterList Chapter_ListView;
    [UIInject("Button_Read_Type")] private UIButton Button_Read_Type;
    [UIInject("Bible_ListView")] private ScrollRect BibleScrollRect;

    private ReadType ReadType => BibleManager.Instance.ReadingData.ReadType;

    private int _lastVerse;

    protected override void Start()
    {
        base.Start();
        BindEvent();
    }

    public override void UpdateContent()
    {
        this.transform.SetAsLastSibling();

        var bibleManager = GameObject.FindAnyObjectByType<BibleManager>();
        if (bibleManager == null)
        {
            var bibleObject = new GameObject("BibleManager");
            bibleObject.AddComponent<BibleManager>();
            BibleManager.Instance.ReadingData.Set(BibleType.Old, 1, 1, 1);
            BibleManager.Instance.ReadingData.SetReadType(ReadType.AI_Reading);
        }

        SwitchActive_Read_Type.Active((int)ReadType);

        _lastVerse = TableDataManager.BibleData.GetVerseCount(BibleManager.Instance.ReadingData.Book, BibleManager.Instance.ReadingData.Chapter);

        Chapter_ListView.gameObject.SetActive(false);

        SetTopUI();

        BibleReadList.CreateList();

        SwitchActive_ReadSelectButtons.HideAll();
    }

    public override void OnClose()
    {
        UnBindEvent();
        base.OnClose();
    }

    public override void BindEvent()
    {
        UIBindEvent.BindEvent(Button_Chapter, OnClickChapter);
        UIBindEvent.BindEvent(Button_Prev, OnClickPrev);
        UIBindEvent.BindEvent(Button_Next, OnClickNext);
        UIBindEvent.BindEvent(Button_Read_Stop, OnClickReadStop);
        UIBindEvent.BindEvent(Button_Back, OnClickBack);
        UIBindEvent.BindEvent(Button_Option, OnClickOption);
        UIBindEvent.BindEvent(Button_Read_Type, OnClickReadType);
        UIBindEvent.BindEvent(Button_Read_Start, OnClickReadStart);
        UIBindEvent.BindEvent(Button_Read_Pause, OnClickReadPause);
        UIBindEvent.BindEvent(Button_Read_Stop, OnClickReadStop);

        if (BibleManager.Instance?.BibleReader != null)
        {
            BibleManager.Instance.BibleReader.OnReadCurrentVerse -= OnReadCurrentVerse;
            BibleManager.Instance.BibleReader.OnReadCurrentVerse += OnReadCurrentVerse;
        }
    }

    public override void UnBindEvent()
    {
        UIBindEvent.UnBindEvent(Button_Chapter, OnClickChapter);
        UIBindEvent.UnBindEvent(Button_Prev, OnClickPrev);
        UIBindEvent.UnBindEvent(Button_Next, OnClickNext);
        UIBindEvent.UnBindEvent(Button_Back, OnClickBack);
        UIBindEvent.UnBindEvent(Button_Option, OnClickOption);
        UIBindEvent.UnBindEvent(Button_Read_Type, OnClickReadType);
        UIBindEvent.UnBindEvent(Button_Read_Start, OnClickReadStart);
        UIBindEvent.UnBindEvent(Button_Read_Pause, OnClickReadPause);
        UIBindEvent.UnBindEvent(Button_Read_Stop, OnClickReadStop);

        if (BibleManager.Instance?.BibleReader != null)
        {
            BibleManager.Instance.BibleReader.OnReadCurrentVerse -= OnReadCurrentVerse;
        }
    }

    private void OnClickReadBeginning()
    {
        BibleManager.Instance.ResetUpdateReadingData();
        BibleManager.Instance.StartReading
                (BibleManager.Instance.ReadingData.Book,
                 BibleManager.Instance.ReadingData.Chapter,
                 BibleManager.Instance.ReadingData.Verse);
    }

    private void OnReadCurrentVerse(int InVerse)
    {
        BibleManager.Instance.CurrentReadingPosition.SetVerse(InVerse);

        var item = BibleReadList.VerseListItem[InVerse - 1];
        ScrollToCenter(item.transform as RectTransform);
    }

    private void ScrollToCenter(RectTransform item)
    {
        Canvas.ForceUpdateCanvases();

        var content = BibleScrollRect.content;
        var viewport = BibleScrollRect.viewport;

        var contentHeight = content.rect.height;
        var viewportHeight = viewport.rect.height;

        if (contentHeight <= viewportHeight)
            return;

        // Content 위에서부터 현재 아이템의 중심까지 거리
        var itemCenterY =
            -item.anchoredPosition.y +
            (item.rect.height * (1f - item.pivot.y));

        // 현재 아이템이 화면 중앙에 오도록 스크롤할 거리
        var scrollY = itemCenterY - (viewportHeight * 0.5f);

        // 스크롤 가능한 전체 거리
        var scrollableHeight = contentHeight - viewportHeight;

        var normalized =
            1f - Mathf.Clamp01(scrollY / scrollableHeight);

        BibleScrollRect.verticalNormalizedPosition = normalized;
    }
}
