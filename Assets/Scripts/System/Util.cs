using UnityEngine;

public static class Util
{
    public static T GetOrAddComponent<T>(this GameObject InGameObject) where T : Component
    {
        var component = InGameObject.GetComponent<T>();

        if (component == null)
            component = InGameObject.AddComponent<T>();

        return component;
    }

    public static float GetUserReadingTime(this string InVerseText)
    {
        if (string.IsNullOrEmpty(InVerseText))
            return 3f;

        var textLength = InVerseText.Replace(" ", "").Length;

        var readingTime = textLength * 0.25f + 0.7f;

        return Mathf.Clamp(readingTime, 4f, 20f);
    }
}