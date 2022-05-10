using Normal.Realtime;

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


        private void OnTriggerStay(UnityEngine.Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!model.isPressed) model.isPressed = true;
                else model.isPressed = false;
            }
        }

        [EasyButtons.Button]
        private void ToggleButton()
        {
            if (pressed) pressed = false;
            else pressed = true;
        }
    }
}
