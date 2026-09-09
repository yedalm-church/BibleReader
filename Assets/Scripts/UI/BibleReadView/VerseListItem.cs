using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VerseListItem : UIBase
{
    [UIInject("Text_Verse_Number")] private TMP_Text Text_Verse_Number;
    [UIInject("Text_Verse")] private TMP_Text Text_Verse;
    [UIInject("VerseListItem(Clone)")] private Image item_Bg;

    [SerializeField] public Sprite ImageNormal;
    [SerializeField] public Sprite ImageSelect;

    private int Verse;
    private string VerseText;

    private ReadType ReadType => BibleManager.Instance?.ReadingData.ReadType ?? ReadType.Max;

    protected override void Awake()
    {
        base.Awake();

        Debug.Log($"ListItem Awake : {name}");
    }

    public void SetData(int InVerse, string InText)
    {
        Verse = InVerse;
        VerseText = InText;

        Text_Verse_Number.text = $"{InVerse}";
        Text_Verse.text = InText;

        item_Bg.sprite = ImageNormal;

        switch(ReadType)
        {
            case ReadType.AI_Reading:
                if (BibleManager.Instance?.BibleReader != null)
                {
                    BibleManager.Instance.BibleReader.OnReadCurrentVerse -= OnReadCurrentVerse;
                    BibleManager.Instance.BibleReader.OnReadCurrentVerse += OnReadCurrentVerse;
                }
                break;
            case ReadType.AlternateReading:
                if (BibleManager.Instance?.BibleReader != null)
                {
                    BibleManager.Instance.AlternateReader.OnReadCurrentVerse -= OnReadCurrentVerse;
                    BibleManager.Instance.AlternateReader.OnReadCurrentVerse += OnReadCurrentVerse;
                }
                break;
        }

    }

    private void OnReadCurrentVerse(int InVerse)
    {
        if (item_Bg == null)
        {
            Debug.LogError($"item_Bg null / Verse:{Verse}");
            return;
        }

        if (InVerse == Verse)
        {
            item_Bg.sprite = ImageSelect;
            AsyncTextEffect();
        }
        else
        {
            item_Bg.sprite = ImageNormal;
        }
    }

    public void OnClear()
    {
        item_Bg.sprite = ImageNormal;

        if (BibleManager.Instance?.BibleReader != null)
            BibleManager.Instance.BibleReader.OnReadCurrentVerse -= OnReadCurrentVerse;

        if (BibleManager.Instance?.BibleReader != null)
            BibleManager.Instance.AlternateReader.OnReadCurrentVerse -= OnReadCurrentVerse;
    }

    private async void AsyncTextEffect()
    {
        var elapsed = 0f;

        var readingTime = VerseText.GetUserReadingTime();

        while (elapsed < readingTime)
        {
            elapsed += Time.fixedDeltaTime;

            var progress = Mathf.Clamp01(elapsed / readingTime);

            Text_Verse.text = VerseText.GetHighlightText(progress);

            await Awaitable.NextFrameAsync();
        }
    }
}
