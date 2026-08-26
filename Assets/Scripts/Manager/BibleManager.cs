using System.Collections.Generic;
using UnityEngine;
using Whisper;
using Whisper.Utils;

public class BibleManager : MonoBehaviour
{
    public static BibleManager Instance { get; private set; }

    private BibleDataLoader _bibleLoader;
    private BibleTTS _bibleTTS;
    private BibleSTT _bibleSTT;
    private BibleReader _bibleReader;
    private BibleAlternateReader _alternateReader;
    private WhisperManager _whisperManager;
    private MicrophoneRecord _microphoneRecord;

    public BibleTTS TTS => _bibleTTS;

    public IReadOnlyList<BibleVerse> Verses => _bibleLoader?.Verses;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        _bibleLoader = new BibleDataLoader();

        if (!_bibleLoader.Load())
        {
            Debug.LogError("성경 데이터 로드 실패");
            return;
        }

        Debug.Log("BibleManager 초기화 완료");

        _whisperManager = new();
        _microphoneRecord = new();

        _bibleTTS = new();
        _bibleTTS.Initialize();

        _bibleSTT = new(_whisperManager, _microphoneRecord);
        _bibleSTT.Initialize();

        _alternateReader = new(_bibleLoader,
                               _bibleTTS,
                               _bibleSTT);

        _bibleReader = new();

        BibleReadingSetting.Load();
    }

    public List<BibleVerse> GetChapter(int InBook, int InChapter)
    {
        return _bibleLoader.GetChapter(InBook, InChapter);
    }

    public BibleVerse GetVerse(int InBook, int InChapter, int InVerse)
    {
        return _bibleLoader.GetVerse(InBook, InChapter, InVerse);
    }

    public int GetVerseCount(int InBook, int InChapter)
    {
        return GetChapter(InBook, InChapter)?.Count ?? -1;
    }

    public void ReadVerse(int InBook, int InChapter, int InVerse)
    {
        var bibleVerse = _bibleLoader.GetVerse(InBook, InChapter, InVerse);

        if (bibleVerse == null)
            return;

        _bibleTTS.Speak(bibleVerse.text);
    }

    public void StopReading()
    {
        _bibleTTS.Stop();
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        _bibleTTS?.Shutdown();
    }

    public void StartReading(int InBook, int InChapter, int InVerse = 1)
    {
        _bibleReader.StartReading(InBook, InChapter, InVerse);
    }

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
