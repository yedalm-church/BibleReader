using UnityEngine;

public partial class BibleManager
{
    private BibleAlternateReader _alternateReader;

    public void StartAlternateReading(int InBook,
                                  int InChapter,
                                  int InVerse)
    {
        _alternateReader.StartReading(InBook, InChapter, InVerse);
    }

    public void UserReadingComplete()
    {
        _alternateReader.UserReadingComplete();
    }
}
