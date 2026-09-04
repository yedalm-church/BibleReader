using System;
using System.Threading;
using UnityEngine;

public class BibleReader
{
    private int _book;
    private int _chapter;
    private int _verse;

    private int _lastVerse;

    private readonly BibleTTS TTS;

    private bool _isReading = false;
    private CancellationTokenSource _readingCts;

    public Action<int> OnReadCurrentVerse;

    public BibleReader(BibleTTS InBibleTTS)
    {
        TTS = InBibleTTS;
    }

    public void StartReading(int InBook, int InChapter, int InVerse = 1)
    {
        _book = InBook;
        _chapter = InChapter;
        _verse = InVerse;

        _lastVerse = TableDataManager.BibleData.GetVerseCount(_book, _chapter);

        TTS.OnSpeakCompleted -= OnSpeakCompleted;
        TTS.OnSpeakCompleted += OnSpeakCompleted;

        _readingCts = new();

        ReadCurrentVerse();
    }

    public async void ReadCurrentVerse()
    {
        await Awaitable.MainThreadAsync();

        var text = TableDataManager.BibleData.GetVerse(_book,
            _chapter,
            _verse)?.text;

        _isReading = true;
        OnReadCurrentVerse?.Invoke(_verse);
        TTS.Speak(text);
    }

    public async void OnSpeakCompleted()
    {
        try
        {
            await Awaitable.MainThreadAsync();

            _verse++;

            if (_verse > _lastVerse)
            {
                OnChapterCompleted();
                return;
            }

            var interval = BibleReadingSetting.Instance.VerseInterval;

            await Awaitable.WaitForSecondsAsync(
                interval,
                _readingCts.Token);


            ReadCurrentVerse();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Wait 취소됨");
        }
        catch (Exception e)
        {
            Debug.LogError($"OnSpeakCompleted Error : {e}");
        }
    }

    private void OnChapterCompleted()
    {
        Debug.Log($"{_chapter}장 통독 완료!");

        _isReading = false;
        _book = 0;
        _chapter = 0;
        _verse = 0;
        _lastVerse = 0;
        _readingCts?.Cancel();

        Debug.Log($"OnChapterCompleted {_chapter} {_verse}");
    }

    public void StopReading()
    {
        _isReading = false;
        _readingCts?.Cancel();
        TTS.Stop();
    }
}