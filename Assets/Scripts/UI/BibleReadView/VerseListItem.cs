using TMPro;
using UnityEngine;

public class VerseListItem : UIBase
{
    [UIInject("Text_Verse_Number")] private TMP_Text Text_Verse_Number;
    [UIInject("Text_Verse")] private TMP_Text Text_Verse;

    public void SetData(int InIndex, string InText)
    {
        Text_Verse_Number.text = $"{InIndex}";
        Text_Verse.text = InText;
    }
}
