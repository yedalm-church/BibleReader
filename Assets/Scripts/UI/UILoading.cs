using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIBase
{
    [UIInject("Image_Loading")] private Image Image_Loading;

    private Coroutine _loadingCoroutine;

    public override void UpdateContent()
    {
        StartLoading();
    }

    protected override void OnDisable()
    {
        StopLoading();
    }

    protected override void OnDestroy()
    {
        StopLoading();
    }

    public void StartLoading()
    {
        if (_loadingCoroutine != null)
            return;

        _loadingCoroutine = StartCoroutine(CoRotateLoading());
    }

    public void StopLoading()
    {
        if (_loadingCoroutine == null)
            return;

        StopCoroutine(_loadingCoroutine);
        _loadingCoroutine = null;
    }

    private IEnumerator CoRotateLoading()
    {
        while (true)
        {
            Image_Loading.transform.Rotate(0f, 0f, -180f * Time.deltaTime);

            yield return null;
        }
    }
}
