using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;



public class TouchToggle : HandTouchEvent
{
    public AvatarRecorder recorder;

    [SerializeField] private bool isOn;

    private void Start()
    {
        HandStartTouchEvent += ToggleTouch;
    }

    private void OnDestroy()
    {
        HandStartTouchEvent -= ToggleTouch;
    }

    private void ToggleTouch(Hand hand)
    {
        if (isOn)
        {
            isOn = false;

            recorder.StopRecording();
        }
        else 
        { 
            isOn = true; 

            recorder.StartRecording();
        }
    }
}
