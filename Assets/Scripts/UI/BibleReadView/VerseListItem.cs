using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VerseListItem : UIBase
{
    [UIInject("Text_Verse_Number")] private TMP_Text Text_Verse_Number;
    [UIInject("Text_Verse")] private TMP_Text Text_Verse;

    private Image item_Bg;

    [SerializeField] public Sprite ImageNormal;
    [SerializeField] public Sprite ImageSelect;

    private int Verse;

    protected override void Awake()
    {
        base.Awake();
        item_Bg = this.GetComponent<Image>();
    }

    public void SetData(int InVerse, string InText)
    {
        Verse = InVerse;
        Text_Verse_Number.text = $"{InVerse}";
        Text_Verse.text = InText;

        item_Bg.sprite = ImageNormal;

        if (BibleManager.Instance?.BibleReader != null)
        {
            BibleManager.Instance.BibleReader.OnReadCurrentVerse -= OnReadCurrentVerse;
            BibleManager.Instance.BibleReader.OnReadCurrentVerse += OnReadCurrentVerse;
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
    }
}
