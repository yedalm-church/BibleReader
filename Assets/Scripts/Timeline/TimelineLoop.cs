using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineLoop : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private float _loopTime = 5f;

    public void Play()
    {
        StartCoroutine(PlayTimeline());
    }

    private IEnumerator PlayTimeline()
    {
        _director.extrapolationMode = DirectorWrapMode.Loop;
        _director.Play();

        yield return new WaitForSeconds(_loopTime);

        _director.Pause();
    }
}