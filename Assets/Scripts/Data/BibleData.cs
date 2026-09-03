using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class BibleVerse
{
    public int book;
    public string code;
    public string name_kr;
    public int chapter;
    public int verse;
    public string text;
    public string source;
}

public class BibleData
{
    private List<BibleVerse> verses = new List<BibleVerse>();

    public IReadOnlyList<BibleVerse> Verses => verses;

    public bool Load()
    {
        var bibleFile = Resources.Load<TextAsset>("Bible/krv_holybible");

        if (bibleFile == null)
        {
            Debug.LogError("성경 파일을 찾지 못했습니다.");
            return false;
        }

        verses.Clear();

        using (StringReader reader = new StringReader(bibleFile.text))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                BibleVerse verse =
                    JsonUtility.FromJson<BibleVerse>(line);

                verses.Add(verse);
            }
        }

        Debug.Log($"성경 로드 완료: {verses.Count}절");

        return true;
    }

    public List<BibleVerse> GetChapter(int InBook, int InChapter)
    {
        return verses.FindAll(v =>
            v.book == InBook &&
            v.chapter == InChapter);
    }

    public int GetChapterCount(int InBook)
    {
        return verses.FindAll(v => v.book == InBook)?.Count ?? 0;
    }

    public BibleVerse GetVerse(int InBook, int InChapter, int InVerse)
    {
        return verses.Find(v =>
            v.book == InBook &&
            v.chapter == InChapter &&
            v.verse == InVerse);
    }

    public int GetVerseCount(int InBook, int InChapter)
    {
        return GetChapter(InBook, InChapter)?.Count ?? -1;
    }
}
