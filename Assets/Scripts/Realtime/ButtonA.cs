using Normal.Realtime;
using UnityEngine;


namespace ETS.Realtime
{
    // Toggles the value of the MultipleButtonsModel's button A if the player enters and exits the trigger volume
    public class ButtonA : RealtimeComponent<MultipleButtonsModel>
    {
        public bool aPressed
        {
            get
            {
                if (model == null) return false;

                return model.buttonAPressed;
            }
            set
            {
                model.buttonAPressed = value;
            }
        }


        private void OnTriggerStay(UnityEngine.Collider other)
        {
            if (other.gameObject.tag == "Player")
                if (!aPressed) aPressed = true;
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            if (other.gameObject.tag == "Player")
                aPressed = false;
        }
    }
}
