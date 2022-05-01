using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ETS
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimeUI : MonoBehaviour
    {

        [SerializeField] private ETS.Realtime.Stopwatch stopwatch;

        void Update()
        {
            if (stopwatch) GetComponent<TMP_Text>().text = stopwatch.time.ToString();
        }
    }
}
