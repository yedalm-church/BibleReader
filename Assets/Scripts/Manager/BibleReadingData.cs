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
        ReadType = InType;
    }

    public void ResetVerse()
    {
        Verse = 1;
    }
}
