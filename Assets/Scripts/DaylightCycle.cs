using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ETS
{
    public class DaylightCycle : MonoBehaviour
    {
        [SerializeField] private GameObject rotatingObj;
        [SerializeField] private ETS.Realtime.Stopwatch stopWatch;

        [SerializeField] private int speed = 10;


        void Start()
        {

        }

        void Update()
        {
            if (rotatingObj && stopWatch)
            {
                // rotate the directional light's transform with synced network time
                rotatingObj.transform.Rotate(Vector3.left, stopWatch.time / speed);          
            }
        }


        float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}
