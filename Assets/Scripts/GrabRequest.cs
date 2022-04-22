using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Normal.Realtime;


namespace ETS.Realtime
{
    public class GrabRequest : MonoBehaviour
    {

        private RealtimeTransform realtimeTransform;
        private RealtimeView realtimeView;
        private XRGrabInteractable grabInteractable;

        private void Start()
        {
            realtimeTransform = GetComponent<RealtimeTransform>();
            realtimeView = GetComponent<RealtimeView>();
            grabInteractable  = GetComponent<XRGrabInteractable>();
        }

        private void Update()
        {
            // need to check if item is already being held
            if (grabInteractable.isSelected)
            {
                realtimeTransform.RequestOwnership();
                realtimeView.RequestOwnership();
            }
        }




    }
}
