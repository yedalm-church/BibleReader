using UnityEngine;

//AI
public partial class BibleManager
{
    private BibleTTS _bibleTTS;
    public BibleTTS TTS => _bibleTTS;

    public void ReadVerse(int InBook, int InChapter, int InVerse)
    {
        var bibleVerse = TableDataManager.BibleDataLoader.GetVerse(InBook, InChapter, InVerse);

        if (bibleVerse == null)
            return;

        _bibleTTS.Speak(bibleVerse.text);
    }

    public void StopReading()
    {
        _bibleTTS.Stop();
    }
}
