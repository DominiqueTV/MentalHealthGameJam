using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ETS.Realtime
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class PlayerRoomCount : MonoBehaviour
    {
        [SerializeField] private RealtimeController controller;
        TMPro.TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMPro.TMP_Text>();
        }

        void Update()
        {
            if (controller && text.text != controller.players.Length.ToString()) text.text = controller.players.Length.ToString();
        }
    }
}
