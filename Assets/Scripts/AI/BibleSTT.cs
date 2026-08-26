using System;
using UnityEngine;
using Whisper;
using Whisper.Utils;

public class BibleSTT
{
    public event Action<string> OnRecognized;
    public event Action OnMatched;

    private WhisperManager _whisperManager;
    private MicrophoneRecord _microphoneRecord;
    private WhisperStream _stream;

    private string _targetText;
    private bool _isMatched;

    public BibleSTT(WhisperManager InWhisperManager, MicrophoneRecord InMicrophoneRecord)
    {
        _whisperManager = InWhisperManager;
        _microphoneRecord = InMicrophoneRecord;
    }

    public async void Initialize()
    {
        Debug.Log($"Whisper ModelPath: {_whisperManager.ModelPath}");
        Debug.Log($"Whisper IsLoaded: {_whisperManager.IsLoaded}");
        Debug.Log($"Whisper IsLoading: {_whisperManager.IsLoading}");

        if (!_whisperManager.IsLoaded)
        {
            await _whisperManager.InitModel();
        }

        Debug.Log($"InitModel 이후 IsLoaded: {_whisperManager.IsLoaded}");

        if (!_whisperManager.IsLoaded)
        {
            Debug.LogError("Whisper 모델 로딩 실패");
            return;
        }

        _stream = await _whisperManager.CreateStream(_microphoneRecord);

        if (_stream == null)
        {
            Debug.LogError("Whisper Stream 생성 실패");
            return;
        }

        _stream.OnResultUpdated += OnResultUpdated;
        _stream.OnSegmentUpdated += OnSegmentUpdated;

        Debug.Log("BibleSTT 초기화 완료");
    }

    public void StartListening(string InTargetText)
    {
        _targetText = Normalize(InTargetText);
        _isMatched = false;

        _microphoneRecord.StartRecord();

        _stream.StartStream();

        Debug.Log($"STT 시작: {_targetText}");
    }

    public void StopListening()
    {
        _microphoneRecord.StopRecord();

        _stream?.StopStream();
    }

    private void OnResultUpdated(string InText)
    {
        if (_isMatched)
            return;

        Debug.Log($"Whisper 중간 결과: {InText}");

        ProcessRecognizedText(InText);
    }

    private void OnSegmentUpdated(WhisperResult InSegment)
    {
        if (_isMatched)
            return;

        Debug.Log($"Whisper 결과: {InSegment.Result}");

        ProcessRecognizedText(InSegment.Result);
    }

    private void ProcessRecognizedText(string InRecognizedText)
    {
        var normalized = Normalize(InRecognizedText);

        OnRecognized?.Invoke(InRecognizedText);

        var similarity = CalculateSimilarity(_targetText, normalized);

        Debug.Log($"성경 구절 일치율: {similarity:P0}");

        if (similarity < 0.7f)
            return;

        _isMatched = true;

        StopListening();

        Debug.Log("사용자 구절 읽기 완료");

        OnMatched?.Invoke();
    }

    private string Normalize(string InText)
    {
        if (string.IsNullOrEmpty(InText))
            return string.Empty;

        return InText.Replace(" ", "")
                 .Replace(",", "")
                 .Replace(".", "")
                 .Replace("!", "")
                 .Replace("?", "")
                 .Trim();
    }

    private float CalculateSimilarity(string InTarget, string InRecognized)
    {
        if (string.IsNullOrEmpty(InTarget) ||
            string.IsNullOrEmpty(InRecognized))
        {
            return 0f;
        }

        int distance = LevenshteinDistance(InTarget, InRecognized);

        int maxLength = Mathf.Max(InTarget.Length, InRecognized.Length);

        return 1f - (float)distance / maxLength;
    }

    private int LevenshteinDistance(string InTextA, string InB)
    {
        int[,] dp = new int[InTextA.Length + 1, InB.Length + 1];

        for (var i = 0; i <= InTextA.Length; i++)
            dp[i, 0] = i;

        for (var j = 0; j <= InB.Length; j++)
            dp[0, j] = j;

        for (var i = 1; i <= InTextA.Length; i++)
        {
            for (var j = 1; j <= InB.Length; j++)
            {
                var cost = InTextA[i - 1] == InB[j - 1]? 0 : 1;

                dp[i, j] = Mathf.Min(Mathf.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
            }
        }

        return dp[InTextA.Length, InB.Length];
    }
}