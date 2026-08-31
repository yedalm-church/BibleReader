using System.Collections.Generic;
using UnityEngine;

public partial class BibleManager
{
    private BibleReader _bibleReader;

    public void StartReading(int InBook, int InChapter, int InVerse = 1)
    {
        _bibleReader.StartReading(InBook, InChapter, InVerse);
    }

    public void RestartCurrentVerse()
    {
        _bibleReader.ReadCurrentVerse();
    }
}
