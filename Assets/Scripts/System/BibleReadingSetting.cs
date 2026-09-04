using UnityEngine;

[CreateAssetMenu(
    fileName = "BibleReadingSetting",
    menuName = "Bible/Bible Reading Setting")]
public class BibleReadingSetting : ScriptableObject
{
    private const string ResourcePath = "Setting/BibleReadingSetting";

    private static BibleReadingSetting _instance;

    public static BibleReadingSetting Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<BibleReadingSetting>(ResourcePath);

            return _instance;
        }
    }

    [Header("TTS")]
    [SerializeField]
    private float _ttsSpeed = 1.0f;

    public float TTSSpeed
    {
        get => _ttsSpeed;
        set => _ttsSpeed = value;
    }

    [Header("Reading")]
    [SerializeField]
    private float _fontSize = 36f;

    public float FontSize
    {
        get => _fontSize;
        set => _fontSize = value;
    }

    [Header("VerseInterval")]
    [SerializeField]
    private float _verseInterval = 1.0f;

    public float VerseInterval
    {
        get => _verseInterval;
        set => _verseInterval = value;
    }

    public static void Load()
    {
        if (_instance != null)
            return;

        _instance = Resources.Load<BibleReadingSetting>(ResourcePath);

        if (_instance == null)
        {
            Debug.LogError($"BibleReadingSetting Load ½ÇÆÐ : {ResourcePath}");
        }
    }
}