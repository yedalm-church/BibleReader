
using System.Diagnostics;

public enum ReadType
{
    AlternateReading,
    AI_Reading,
    Record,
    Max
}

public class BibleReadingData
{
    public ReadType ReadType;
    public BibleType BibleType;
    public int Book;
    public int Chapter;
    public int Verse;

    public void Set(BibleType InType, int InBook, int InChapter, int InVerse)
    {
        BibleType = InType;
        Book = InBook;
        Chapter = InChapter;
        Verse = InVerse;
    }

    public void SetReadType(ReadType InType)
    {
        if (InType == ReadType.Max)
        {
            Debug.WriteLine($"{InType} is error");
            return;
        }
        ReadType = InType;
    }

    public void ResetVerse()
    {
        Verse = 1;
    }

    public bool IsValidReadType(ReadType InType)
    {
        return InType >= 0 && InType < ReadType.Max;
    }
}
