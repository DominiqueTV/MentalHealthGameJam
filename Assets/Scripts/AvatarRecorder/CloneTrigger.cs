using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(Collider))]
public class CloneTrigger : MonoBehaviour
{
   [SerializeField] private Collider triggerVolume;
   [SerializeField] private List<PlayableDirector> directors;

    private void Start()
    {
        triggerVolume = GetComponent<Collider>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (triggerVolume != null)
                if (directors != null)
                    foreach (var director in directors) 
                    { 
                        director.Play();
                        director.gameObject.SetActive(true);
                    }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (triggerVolume != null)
                if (directors != null)
                    foreach (var director in directors)
                    {
                        director.Stop();
                        director.gameObject.SetActive(false);
                    }
    }
}
