using Normal.Realtime;
using UnityEngine;

namespace ETS.Realtime
{
    // Toggles the value of the MultipleButtonsModel's button B if the player enters and exits the trigger volume
    public class ButtonB : RealtimeComponent<MultipleButtonsModel>
    {
        public bool bPressed
        {
            get
            {
                if (model == null) return false;

                return model.buttonBPressed;
            }
            set
            {
                model.buttonBPressed = value;
            }
        }

        private void OnTriggerStay(UnityEngine.Collider other)
        {
            if (other.gameObject.tag == "Player")
                if (!bPressed) bPressed = true;
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            if (other.gameObject.tag == "Player")
                bPressed = false;
        }
    }
}
