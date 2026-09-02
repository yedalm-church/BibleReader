using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ReadType
{
    AlternateReading,
    AI_Reading,
    Record,
}

public class BibleReadingData
{
    public ReadType ReadType;
    public BibleType Type;
    public int Book;
    public int Chapter;
    public int Verse;
}

public partial class BibleManager
{
    public BibleReadingData ReadingData { get; private set; }

    public static (int Book, int Chapter, int Verse) UpdateReadingData = (1, 1, 1);

    public readonly List<(ReadType Type, string Text)> ReadTypeText = new()
    {
        (ReadType.AlternateReading, "AI¿Í ±³´ë·Î ÀÐ±â"),
        (ReadType.AI_Reading, "AI ³¶µ¶ µè±â"),
        (ReadType.Record, "³» ³¶µ¶ ³ìÀ½"),
    };

    public void SetReadingData(BibleType InType, int InBook, int InChapter, int InVerse)
    {
        ReadingData = new BibleReadingData
        {
            Type = InType,
            Book = InBook,
            Chapter = InChapter,
            Verse = InVerse,
        };
    }

    public void SetReadingType(ReadType InType)
    {
        ReadingData.ReadType = InType;
    }

    public string GetReadTypeText(ReadType InType)
    {
        return ReadTypeText.First(x => x.Type == InType).Text;
    }

    public void UpdateVerseReadingData(int InVerse)
    {
        UpdateReadingData.Verse = InVerse;
    }

    public void ResetReadingVerseData()
    {
        ReadingData.Verse = 1;
    }

    public void ResetUpdateReadingData()
    {
        UpdateReadingData.Book = ReadingData.Book;
        UpdateReadingData.Chapter = ReadingData.Chapter;
        UpdateReadingData.Verse = ReadingData.Verse;
    }
}
