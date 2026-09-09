using UnityEngine;

public static class TextExtensions
{
    public static string GetHighlightText(this string InText, float InProgress)
    {
        InProgress = Mathf.Clamp01(InProgress);

        var index = Mathf.FloorToInt(InText.Length * InProgress);

        var completed = InText.Substring(0, index);
        var remaining = InText.Substring(index);

        return $"<color=#43270F>{completed}</color>" +
               $"<color=#CFC6B8>{remaining}</color>";
    }
}
