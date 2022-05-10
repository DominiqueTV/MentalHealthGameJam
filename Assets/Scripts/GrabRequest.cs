using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Normal.Realtime;
using Autohand;

namespace ETS.Realtime
{
    public class GrabRequest : MonoBehaviour
    {

        private RealtimeTransform realtimeTransform;
        private RealtimeView realtimeView;
        private XRGrabInteractable grabInteractable;
        private Grabbable grabbable;

        private void Start()
        {
            realtimeTransform = GetComponent<RealtimeTransform>();
            realtimeView = GetComponent<RealtimeView>();
            grabInteractable  = GetComponent<XRGrabInteractable>();
            grabbable = GetComponent<Grabbable>();
        }

        private void Update()
        {
            // need to check if item is already being held
            if (grabInteractable && grabInteractable.isSelected)
            {
                if (!realtimeTransform.isOwnedRemotelySelf || realtimeTransform.isUnownedSelf)
                {
                    realtimeTransform.RequestOwnership();
                    //realtimeView.RequestOwnership();
                }
            }

            if (grabbable && grabbable.IsHeld())
            {
                if (!realtimeTransform.isOwnedRemotelySelf || realtimeTransform.isUnownedSelf)
                {
                    realtimeTransform.RequestOwnership();
                    //realtimeView.RequestOwnership();
                }
            }
        }




    }
}
