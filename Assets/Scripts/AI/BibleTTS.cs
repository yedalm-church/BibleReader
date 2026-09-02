using System;
using System.Diagnostics;
using UnityEngine;

public class BibleTTS
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private AndroidJavaObject _tts;
    private AndroidJavaObject _activity;
#endif

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    private Process _ttsProcess;
#endif

    public event Action OnSpeakCompleted;

    public void Initialize()
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        using (AndroidJavaClass unityPlayer =
               new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            _activity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        _activity.Call(
            "runOnUiThread",
            new AndroidJavaRunnable(() =>
            {
                AndroidJavaProxy listener =
                    new TTSInitListener(this);

                _tts = new AndroidJavaObject(
                    "android.speech.tts.TextToSpeech",
                    _activity,
                    listener
                );
            })
        );

#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        UnityEngine.Debug.Log("Windows TTS 준비 완료");

#else

        UnityEngine.Debug.LogWarning("현재 플랫폼에서는 TTS를 지원하지 않습니다.");

#endif
    }

    public void Speak(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

#if UNITY_ANDROID && !UNITY_EDITOR

        if (_tts == null)
            return;

        _tts.Call<int>(
            "speak",
            text,
            0,
            null,
            "BibleTTS"
        );

#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        Stop();

        var escapedText = text.Replace("'", "''");

        var rate = BibleReadingSetting.Instance.TTSSpeed;

        var command =
            "Add-Type -AssemblyName System.Speech; " +
            "$speak = New-Object System.Speech.Synthesis.SpeechSynthesizer; " +
            $"$speak.Rate = {rate}; " +
            "$speak.Speak('" + escapedText + "');";

        var startInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = "-NoProfile -Command \"" + command + "\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        _ttsProcess = new Process();
        _ttsProcess.StartInfo = startInfo;

        // 프로세스 종료를 감지할 수 있게 함
        _ttsProcess.EnableRaisingEvents = true;

        // "음성 재생이 끝난 시점"
        _ttsProcess.Exited += (sender, args) =>
        {
            OnSpeakCompleted?.Invoke();
        };

        UnityEngine.Debug.Log($"_ttsProcess {text}");
        _ttsProcess.Start();

#endif
    }

    public void Stop()
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        _tts?.Call<int>("stop");

#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

        if (_ttsProcess == null)
            return;

        try
        {
            if (!_ttsProcess.HasExited)
                _ttsProcess.Kill();
        }
        catch (InvalidOperationException)
        {
            // 이미 종료됐거나 Process와 연결되지 않은 상태
        }
        finally
        {
            _ttsProcess.Dispose();
            _ttsProcess = null;
        }

#endif
    }

    public void Shutdown()
    {
        Stop();

#if UNITY_ANDROID && !UNITY_EDITOR

        if (_tts != null)
        {
            _tts.Call("shutdown");
            _tts.Dispose();
            _tts = null;
        }

#endif
    }

#if UNITY_ANDROID && !UNITY_EDITOR

    private class TTSInitListener : AndroidJavaProxy
    {
        private readonly BibleTTS _owner;

        public TTSInitListener(BibleTTS owner)
            : base("android.speech.tts.TextToSpeech$OnInitListener")
        {
            _owner = owner;
        }

        public void onInit(int status)
        {
            if (status != 0)
            {
                UnityEngine.Debug.LogError("Android TTS 초기화 실패");
                return;
            }

            using AndroidJavaObject locale =
                new AndroidJavaObject(
                    "java.util.Locale",
                    "ko",
                    "KR"
                );

            _owner._tts.Call<int>(
                "setLanguage",
                locale
            );

            UnityEngine.Debug.Log(
                "Android 한국어 TTS 초기화 완료");
        }
    }

#endif
}