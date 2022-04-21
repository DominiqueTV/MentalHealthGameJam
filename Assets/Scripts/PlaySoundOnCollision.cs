using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ETS
{
    [RequireComponent(typeof(AudioSource))]
    public class PlaySoundOnCollision : MonoBehaviour
    {

        private AudioSource audioSource;


        private void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // play collision sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.Play();
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}
