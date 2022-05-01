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

        [SerializeField] private float time;
        [SerializeField] private float timeInterval = 10;
        [SerializeField] private bool didCheck = false;
        [SerializeField] public GameObject[] players;

        Normal.Realtime.Realtime realtime;


        private void Start()
        {
            realtime = GetComponent<Normal.Realtime.Realtime>();
        }

        private void LateUpdate()
        {
            // find players every so often
            time += Time.deltaTime;
            if (Mathf.RoundToInt(time) % timeInterval == 0 && !didCheck)
            {
                CheckForPlayers();
                didCheck = true;
            }
            else { didCheck = false; }
        }

        void CheckForPlayers()
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (debug) Debug.Log("Reloading Player List");
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
