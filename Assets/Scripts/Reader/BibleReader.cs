using UnityEngine;

public class BibleReader
{
    private int _book;
    private int _chapter;
    private int _verse;

    private int _lastVerse;

    public void StartReading(int InBook, int InChapter, int InVerse = 1)
    {
        _book = InBook;
        _chapter = InChapter;
        _verse = InVerse;

        _lastVerse = TableDataManager.BibleDataLoader.GetVerseCount(_book, _chapter);

        BibleManager.Instance.TTS.OnSpeakCompleted += OnSpeakCompleted;

        ReadCurrentVerse();
    }

    public void ReadCurrentVerse()
    {
        var text = TableDataManager.BibleDataLoader.GetVerse(_book,
            _chapter,
            _verse)?.text;

        BibleManager.Instance.TTS.Speak(text);
    }

    public void OnSpeakCompleted()
    {
        _verse++;

        if (_verse > _lastVerse)
        {
            OnChapterCompleted();
            return;
        }

        ReadCurrentVerse();
    }

    private void OnChapterCompleted()
    {
        Debug.Log($"{_book}장 통독 완료!");

        _book = 0;
        _chapter = 0;
        _verse = 0;
        _lastVerse = 0;

        BibleManager.Instance.TTS.OnSpeakCompleted -= OnSpeakCompleted;
    }
}