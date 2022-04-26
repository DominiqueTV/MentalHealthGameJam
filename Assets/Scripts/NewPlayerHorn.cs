using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ETS
{
    public class NewPlayerHorn : MonoBehaviour
    {
        Normal.Realtime.Realtime realtime;
        [SerializeField] private AudioSource audioSource;

        public AudioClip enterClip;
        public AudioClip exitClip;

        private bool playedEnterSound = false;
        private bool playedExitSound = false;

        private void Start()
        {
            realtime = FindObjectOfType<Normal.Realtime.Realtime>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (realtime.connected && !playedEnterSound)
            {
                PlayEnterSound();
                playedEnterSound = true;
                playedExitSound = false;
            }

            if (realtime.disconnected && !playedExitSound)
            {
                PlayExitSound();
                playedExitSound = true;
                playedEnterSound = false;
            }
        }

        void PlayEnterSound()
        {
            if (enterClip)
                audioSource.PlayOneShot(enterClip);
        }

        void PlayExitSound()
        {
            if (exitClip)
                audioSource.PlayOneShot(exitClip);
        }



    }
}
