using UnityEngine;

public class CurrentReadingPosition
{
    public int Book;
    public int Chapter;
    public int Verse;

    public (int Book, int Chapter, int Verse) Value => (Book, Chapter, Verse);

    public void SetVerse(int InVerse)
    {
        Verse = InVerse;
    }
}
