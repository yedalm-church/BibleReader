using System;
using UnityEngine;

public class BibleAlternateReader
{
    private readonly BibleDataLoader _bibleLoader;
    private readonly BibleTTS _bibleTTS;
    private readonly BibleSTT _bibleSTT;

    private int _currentBook;
    private int _currentChapter;
    private int _currentVerse;
    private bool _isUserTurn;

    public bool IsUserTurn => _isUserTurn;
    public int CurrentVerse => _currentVerse;

    public event Action<BibleVerse> OnUserTurn;
    public event Action<BibleVerse> OnAITurn;
    public event Action OnChapterFinished;
    public event Action OnSpeakCompleted;

    public BibleAlternateReader(BibleDataLoader InBibleLoader, BibleTTS InBibleTTS, BibleSTT InBibleSTT)
    {
        _bibleLoader = InBibleLoader;
        _bibleTTS = InBibleTTS;
        _bibleSTT = InBibleSTT;

        // AI가 읽기를 끝내면 자동 호출
        _bibleTTS.OnSpeakCompleted += AIReadingComplete;

        // 사용자가 해당 구절을 읽었다고 인식되면 자동 호출
        _bibleSTT.OnMatched += UserReadingComplete;
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

    private void ReadCurrentVerse()
    { 
        var verse = _bibleLoader.GetVerse(_currentBook, _currentChapter, _currentVerse);

        if (verse == null)
        {
            OnChapterFinished?.Invoke();
            return;
        }

        if (_isUserTurn)
        {
            // 사용자 차례
            Debug.Log($"사용자 {_currentVerse}절 - {verse.text}");

            _bibleSTT.StartListening(verse.text);
        }
        else
        {
            // AI 차례
            Debug.Log($"AI {_currentVerse}절 - {verse.text}");

            _bibleSTT.StopListening();

            _bibleTTS.Speak(verse.text);
        }
    }
}