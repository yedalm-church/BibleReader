using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadManager
{
    private static readonly Stack<string> _history = new();

    public static void LoadScene(string InSceneName)
    {
        var currentScene = SceneManager.GetActiveScene().name;

        _history.Push(currentScene);

        SceneManager.LoadScene(InSceneName);
    }

    public static async Awaitable LoadSceneAsync(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName);

        if (operation == null)
        {
            Debug.LogError($"Scene Load ½ÇÆÐ : {sceneName}");
            return;
        }

        while (!operation.isDone)
        {
            await Awaitable.NextFrameAsync();
        }
    }

    public static void Back()
    {
        if (_history.Count == 0)
            return;

        var previousScene = _history.Pop();

        SceneManager.LoadScene(previousScene);
    }
}