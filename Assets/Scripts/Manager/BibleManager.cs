using System.Collections.Generic;
using UnityEngine;
using Whisper;
using Whisper.Utils;

public partial class BibleManager : MonoBehaviour
{
    public static BibleManager Instance { get; private set; }

    private WhisperManager _whisperManager;
    private MicrophoneRecord _microphoneRecord;

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
        TableDataManager.Initialize();
        UIManager.Initialize();

        Debug.Log("BibleManager 초기화 완료");

        ReadingData = new();
        CurrentReadingPosition = new();

        _whisperManager = new();
        _microphoneRecord = new();

        _bibleSTT = new(_whisperManager, _microphoneRecord);
        _bibleSTT.Initialize();

        _bibleTTS = new();
        _bibleTTS.Initialize();

        _alternateReader = new(_bibleTTS, _bibleSTT);
        _bibleReader = new(_bibleTTS);

        BibleReadingSetting.Load();
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        _bibleTTS?.Shutdown();
    }

    private bool _wasAISpeaking = false;

    //private void OnApplicationPause(bool pauseStatus)
    //{
    //    if (pauseStatus)
    //    {
    //        Debug.Log("앱 멈춤");

    //        _wasAISpeaking = true;

    //        if (_wasAISpeaking)
    //            StopReading();
    //    }
    //    else
    //    {
    //        Debug.Log("앱 다시 시작");

    //        if (_wasAISpeaking)
    //        {
    //            _wasAISpeaking = false;
    //            RestartCurrentVerse();
    //        }
    //    }
    //}
}
