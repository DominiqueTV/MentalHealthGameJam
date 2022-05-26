using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evereal.VideoCapture;
using UnityEngine.Timeline;

public partial class MicRecorder : AudioCapture
{
    public bool debug;

    [EasyButtons.Button]
    public string GetLastAudioClip()
    {
        if (debug) Debug.Log(audioRecorder.GetRecordedAudio());
        return audioRecorder.GetRecordedAudio();
    }

}
