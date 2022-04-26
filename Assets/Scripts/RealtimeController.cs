using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal;
using EasyButtons;

namespace ETS.Realtime
{

    [RequireComponent(typeof(Normal.Realtime.Realtime))]
    public class RealtimeController : MonoBehaviour
    {
        Normal.Realtime.Realtime realtime;




        private void Start()
        {
            realtime = GetComponent<Normal.Realtime.Realtime>();
        }

        [Button]
        public void ConnectToRoom(string roomName)
        {
            realtime.Connect(roomName);
        }

        [Button]
        public void Disconnect()
        {
            realtime.Disconnect();
        }
    }
}
