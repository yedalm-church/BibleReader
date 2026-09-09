using System;
using Unity.VisualScripting;
using UnityEngine;

public class BibleAlternateReader
{
    private readonly BibleTTS TTS;
    private readonly BibleSTT STT;

    private int _currentBook;
    private int _currentChapter;
    private int _currentVerse;
    private bool _isUserTurn;

    public bool IsUserTurn => _isUserTurn;
    public int CurrentVerse => _currentVerse;

    public Action<BibleVerse> OnUserTurn;
    public Action<BibleVerse> OnAITurn;
    public Action OnChapterFinished;
    public Action OnSpeakCompleted;
    public Action<int> OnReadCurrentVerse;

    public BibleAlternateReader(BibleTTS InBibleTTS, BibleSTT InBibleSTT)
    {
        TTS = InBibleTTS;
        STT = InBibleSTT;

        // AI가 읽기를 끝내면 자동 호출
        TTS.OnSpeakCompleted += AIReadingComplete;

        // 사용자가 해당 구절을 읽었다고 인식되면 자동 호출
        STT.OnMatched += UserReadingComplete;
    }

    public void StartReading(int InBook, int InChapter, int InVerse)
    {
        _currentBook = InBook;
        _currentChapter = InChapter;
        _currentVerse = InVerse;

        // 처음은 AI 차례
        _isUserTurn = false;

        ReadCurrentVerse();
    }

    /// <summary>
    /// 사용자가 자신의 절을 다 읽었을 때 호출
    /// </summary>
    public void UserReadingComplete()
    {
        if (!_isUserTurn)
        {
            Debug.LogWarning("현재는 사용자 차례가 아닙니다.");
            return;
        }

        // 다음 절은 AI 차례
        _currentVerse++;
        _isUserTurn = false;

        // 바로 AI가 다음 절 읽음
        ReadCurrentVerse();
    }

    /// <summary>
    /// AI가 자신의 절을 다 읽었을 때 호출
    /// </summary>
    public void AIReadingComplete()
    {
        if (_isUserTurn)
            return;

        // 다음 절은 사용자 차례
        _currentVerse++;
        _isUserTurn = true;

        ReadCurrentVerse();
    }

    private async void ReadCurrentVerse()
    {
        await Awaitable.MainThreadAsync();

        var verse = TableDataManager.BibleData.GetVerse(_currentBook, _currentChapter, _currentVerse);

        if (verse == null)
        {
            OnChapterFinished?.Invoke();
            return;
        }


        if (_isUserTurn)
        {
            // 사용자 차례
            Debug.Log($"사용자 {_currentVerse}절 - {verse.text}");

            OnReadCurrentVerse?.Invoke(verse.verse);

            STT.StartListening(verse.text);

            //StartUserTurn(verse.text);
        }
        else
        {
            // AI 차례
            Debug.Log($"AI {_currentVerse}절 - {verse.text}");

            OnReadCurrentVerse?.Invoke(verse.verse);

            STT.StopListening();
            TTS.Speak(verse.text);
        }
    }

    private async void StartUserTurn(string InVerseText)
    {
        var readingTime = InVerseText.GetUserReadingTime();

        Debug.Log($"사용자 읽기 시간: {readingTime:F1}초");

        await Awaitable.WaitForSecondsAsync(readingTime);

        UserReadingComplete();
    }
}