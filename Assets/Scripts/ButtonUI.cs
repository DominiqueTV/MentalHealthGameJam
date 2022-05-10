using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ETS
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private ETS.Realtime.Button button;


        void Update()
        {
            if (button) GetComponent<TMP_Text>().text = button.pressed.ToString();
        }
    }
}
