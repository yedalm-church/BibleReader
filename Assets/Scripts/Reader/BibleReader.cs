using System;
using UnityEngine;

public class BibleReader
{
    private int _book;
    private int _chapter;
    private int _verse;

    private int _lastVerse;

    public Action<int> OnReadCurrentVerse;

    public void StartReading(int InBook, int InChapter, int InVerse = 1)
    {
        _book = InBook;
        _chapter = InChapter;
        _verse = InVerse;

        _lastVerse = TableDataManager.BibleDataLoader.GetVerseCount(_book, _chapter);

        BibleManager.Instance.TTS.OnSpeakCompleted -= OnSpeakCompleted;
        BibleManager.Instance.TTS.OnSpeakCompleted += OnSpeakCompleted;

        ReadCurrentVerse();
    }

    public async void ReadCurrentVerse()
    {
        await Awaitable.MainThreadAsync();

        var text = TableDataManager.BibleDataLoader.GetVerse(_book,
            _chapter,
            _verse)?.text;

        Debug.Log($"ReadCurrentVerse {_chapter} {_verse} {text}");
        if (_verse == 2)
        {
            Debug.Log("hjlee");
        }
        OnReadCurrentVerse?.Invoke(_verse);
        BibleManager.UpdateReadingData = (_book, _chapter, _verse);

        BibleManager.Instance.TTS.Speak(text);
    }

    public void OnSpeakCompleted()
    {
        _verse++;

        if (_verse > _lastVerse)
        {
            Debug.Log($"OnSpeakCompleted {_lastVerse} {_verse}");
            OnChapterCompleted();
            return;
        }

        Debug.Log($"OnSpeakCompleted {_chapter} {_verse}");
        ReadCurrentVerse();
    }

    private void OnChapterCompleted()
    {
        Debug.Log($"{_chapter}장 통독 완료!");

        _book = 0;
        _chapter = 0;
        _verse = 0;
        _lastVerse = 0;

        Debug.Log($"OnChapterCompleted {_chapter} {_verse}");
    }
}