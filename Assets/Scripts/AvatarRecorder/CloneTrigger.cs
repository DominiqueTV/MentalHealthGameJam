using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(Collider))]
public class CloneTrigger : MonoBehaviour
{
   [SerializeField] private Collider triggerVolume;
   [SerializeField] private PlayableDirector director;

    private void Start()
    {
        triggerVolume = GetComponent<Collider>();   
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (triggerVolume != null)
                if(director != null)
                    if (!director.playableGraph.IsPlaying())
                        director.Play();
                    else
                        director.Stop();
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (triggerVolume != null)
                if (director != null)
                    director.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (triggerVolume != null)
                if (director != null)
                    director.Stop();
    }
}
