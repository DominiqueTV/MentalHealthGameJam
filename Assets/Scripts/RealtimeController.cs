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

        [SerializeField] private bool debug;

        Normal.Realtime.Realtime realtime;

        [SerializeField] private GameObject[] players;

        private bool didUpdateList = false;
        private bool didSendConnectEvent = false;
        private bool didSendDisconnectEvent = false;

        public delegate void PlayerEnteredRoom();
        public static event PlayerEnteredRoom onPlayerEnteredRoom;

        public delegate void PlayerExitedRoom();
        public static event PlayerExitedRoom onPlayerExitedRoom;


        private void Start()
        {
            realtime = GetComponent<Normal.Realtime.Realtime>();
        }



        void UpdatePlayerList()
        {
            // Not an optimal way of finding players in the scene
            players = GameObject.FindGameObjectsWithTag("Player");
            didUpdateList = true;
        }

        void InvokeConnectedEvent()
        {
            onPlayerEnteredRoom.Invoke();
            didSendConnectEvent = true;
        }

        void InvokeDisconnectedEvent()
        {
            onPlayerExitedRoom.Invoke();
            didSendDisconnectEvent = true;
        }


        private void Update()
        {
            if (realtime.connected && !didUpdateList && !didSendConnectEvent)
            {
                UpdatePlayerList();
                InvokeConnectedEvent();
            }

            if (realtime.disconnected && !didUpdateList && !didSendDisconnectEvent)
            {
                UpdatePlayerList();
                InvokeDisconnectedEvent();
            }
        }


        // Button controls
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
