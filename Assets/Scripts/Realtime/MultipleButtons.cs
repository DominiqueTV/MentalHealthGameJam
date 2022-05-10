using Normal.Realtime;
using UnityEngine;
using UnityEngine.Events;

namespace ETS.Realtime
{
    
    public class MultipleButtons : RealtimeComponent<MultipleButtonsModel>
    {
        [SerializeField] public ButtonA buttonA;
        [SerializeField] public ButtonB buttonB;


        [SerializeField] private UnityEvent BothButtonsArePressed;
        [SerializeField] private UnityEvent BothButtonsAreNotPressed;
        [SerializeField] private bool didInvoke = false;


        private void Update()
        {
            // Invokes the unity event if both buttons are pressed
            if (buttonA != null && buttonB != null)
            {
                if (buttonA.aPressed && buttonB.bPressed && !didInvoke)
                {
                    BothButtonsArePressed.Invoke();
                    didInvoke = true;
                }

                if (!buttonA.aPressed || !buttonB.bPressed)
                {
                    BothButtonsAreNotPressed.Invoke(); 
                    didInvoke = false;
                }
            }
        }



        // Editor Buttons
        [EasyButtons.Button]
        private void ToggleButtonA()
        {
            if (buttonA && buttonA.aPressed) buttonA.aPressed = false;
            else buttonA.aPressed = true;
        }

        [EasyButtons.Button]
        private void ToggleButtonB()
        {
            if (buttonB && buttonB.bPressed) buttonB.bPressed = false;
            else buttonB.bPressed = true;
        }
    }
    
}