using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadManager
{
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
}