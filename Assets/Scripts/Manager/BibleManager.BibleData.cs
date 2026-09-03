using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ReadType
{
    AlternateReading,
    AI_Reading,
    Record,
}

public partial class BibleManager
{
    public BibleReadingData ReadingData { get; private set; }

    public CurrentReadingPosition CurrentReadingPosition { get; private set; }

    public readonly List<(ReadType Type, string Text)> ReadTypeText = new()
    {
        (ReadType.AlternateReading, "AI¿Í ±³´ë·Î ÀÐ±â"),
        (ReadType.AI_Reading, "AI ³¶µ¶ µè±â"),
        (ReadType.Record, "³» ³¶µ¶ ³ìÀ½"),
    };

    public string GetReadTypeText(ReadType InType)
    {
        return ReadTypeText.First(x => x.Type == InType).Text;
    }

    public void ResetUpdateReadingData()
    {
        CurrentReadingPosition.Book = ReadingData.Book;
        CurrentReadingPosition.Chapter = ReadingData.Chapter;
        CurrentReadingPosition.Verse = ReadingData.Verse;
    }
}
