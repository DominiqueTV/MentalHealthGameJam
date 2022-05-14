using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using UnityEngine.Events;

public class TouchToggle : HandTouchEvent
{
    public AvatarRecorderV2 recorder;

    [SerializeField] private bool isOn;

    public UnityEvent ToggleOn;
    public UnityEvent ToggleOff;

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

            ToggleOff?.Invoke();
        }
        else 
        { 
            isOn = true; 

            ToggleOn?.Invoke();
        }
    }
}
