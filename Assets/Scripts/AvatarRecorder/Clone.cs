using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Clone : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    private void OnEnable()
    {
        if (!director) director = GetComponentInChildren<PlayableDirector>();
    }


    [EasyButtons.Button]
    private void Play()
    {
        director.Play();
    }

    [EasyButtons.Button]
    private void Stop()
    {
        director.Stop();
    }

    [EasyButtons.Button]
    private void ResetAndPlay()
    {
        director.time = 0;
        director.Play();
    }

    [EasyButtons.Button]
    private void Pause()
    {
        if (director.state == PlayState.Playing) 
            director.Pause();
    }

    [EasyButtons.Button]
    private void Resume()
    {
        if (director.state == PlayState.Paused) 
            director.Resume();
    }
}
