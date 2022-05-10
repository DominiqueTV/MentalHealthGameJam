using Normal.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace ETS.Realtime
{
    public class Button : RealtimeComponent<ButtonModel>
    {

        public bool pressed 
        { 
            get 
            { 
                if (model == null) return false;

                return model.isPressed;
            }
            private set
            {
                model.isPressed = value;
            }
        }


        [SerializeField] private UnityEvent ButtonIsPressed;
        [SerializeField] private UnityEvent ButtonIsNotPressed;
        [SerializeField] private bool didInvoke = false;


        private void Update()
        {
            // Invokes the unity event if the button is pressed
            if(pressed && !didInvoke)
            {
                ButtonIsPressed.Invoke();
                didInvoke = true;
            }

            if (!pressed && didInvoke) 
            {
                ButtonIsNotPressed.Invoke();
                didInvoke = false;
            }
            
        }


        private void OnTriggerStay(UnityEngine.Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!pressed)
                {
                    pressed = true;
                }
            }
        }


        private void OnTriggerExit(UnityEngine.Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                pressed = false;
            }
        }
        


        // Editor Buttons
        [EasyButtons.Button]
        private void ToggleButton()
        {
            if (pressed) pressed = false;
            else pressed = true;
        }
    }
}
