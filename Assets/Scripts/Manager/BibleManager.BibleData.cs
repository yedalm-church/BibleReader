using UnityEngine;

public class BibleReadingData
{
    public BibleType Type;
    public int BookIndex;
    public int Chapter;
    public int Verse;
}

public partial class BibleManager : MonoBehaviour
{
    public BibleReadingData ReadingData { get; private set; }

    public void SetReadingData(BibleType InType, int InBookIndex, int InChapter, int InVerse)
    {
        ReadingData = new BibleReadingData
        {
            Type = InType,
            BookIndex = InBookIndex,
            Chapter = InChapter,
            Verse = InVerse,
        };
    }
}
